-- Creating mockup data in order to test our website etc.

-- Disable row count messages to keep the output clean
SET NOCOUNT ON;

-- Clean up any existing data in case of re-runs
TRUNCATE TABLE SCOUT_ACTIVITY;
DELETE FROM ACTIVITY_SCHEDULE;
DELETE FROM ACTIVITY;
TRUNCATE TABLE SCOUT_SKILL;
DELETE FROM SKILL;
DELETE FROM SCOUT;
DELETE FROM [USER];

-- Temporary storage tables to manage IDs dynamically
DECLARE @LeaderIdMap TABLE (UserId INT, RowNum INT IDENTITY(1,1));
DECLARE @ScoutIdMap TABLE (UserId INT, Age INT);
DECLARE @SkillIdMap TABLE (SkillId INT, SkillName NVARCHAR(100));
DECLARE @ActivityIdMap TABLE (ActivityId INT, ActivityName NVARCHAR(100));
DECLARE @ScheduleIdMap TABLE (ScheduleId INT, ActivityName NVARCHAR(100), RowNum INT IDENTITY(1,1));

-- =========================================================================
-- 1. INSERT 3 LEADERS
-- =========================================================================
INSERT INTO [USER] (FirstName, LastName, IsLeader, Email, [Password])
OUTPUT inserted.UserId INTO @LeaderIdMap (UserId)
VALUES 
('Lucas', 'Smith', 1, 'lucas.smith@scouts.org', 'Hash_SecurePassword123!'),
('Peter', 'Jones', 1, 'peter.jones@scouts.org', 'Hash_SecurePassword123!'),
('Jacob', 'Taylor', 1, 'jacob.taylor@scouts.org', 'Hash_SecurePassword123!');

-- To satisfy the SCOUT_ACTIVITY FK constraint, Leaders must temporarily exist in SCOUT table
INSERT INTO SCOUT (UserId, Age)
SELECT UserId, 35 FROM @LeaderIdMap;

-- =========================================================================
-- 2. INSERT 20 SCOUTS
-- =========================================================================
MERGE INTO [USER] AS Target
USING (VALUES
    ('Emma', 'Brown', 9), ('Liam', 'Wilson', 16), ('Olivia', 'Johnson', 10), 
    ('Noah', 'Davies', 9), ('Ava', 'Robinson', 11), ('Oliver', 'Wright', 14), 
    ('Sophia', 'Thompson', 12), ('Elijah', 'Evans', 15), ('Isabella', 'Walker', 17), 
    ('James', 'Green', 14), ('Mia', 'Hall', 11), ('Benjamin', 'Adams', 20), 
    ('Charlotte', 'Baker', 17), ('William', 'Clark', 10), ('Amelia', 'Lewis', 19), 
    ('Alexander', 'Hill', 15), ('Evelyn', 'Scott', 13), ('Michael', 'King', 16), 
    ('Abigail', 'Allen', 8), ('Daniel', 'Young', 18)
) AS Source (FirstName, LastName, Age)
ON 1 = 0 
WHEN NOT MATCHED THEN
    INSERT (FirstName, LastName, IsLeader, Email, [Password])
    VALUES (Source.FirstName, Source.LastName, 0, LOWER(Source.FirstName + '.' + Source.LastName + '@scoutmail.com'), 'Hash_ScoutPass!')
    OUTPUT inserted.UserId, Source.Age INTO @ScoutIdMap (UserId, Age);

INSERT INTO SCOUT (UserId, Age)
SELECT UserId, Age FROM @ScoutIdMap;

-- =========================================================================
-- 3. INSERT 8 SKILLS
-- =========================================================================
MERGE INTO SKILL AS Target
USING (VALUES
    ('Horse Riding'), ('Fire Security'), ('Woodcutting'), ('First Aid'),
    ('Orienteering'), ('Knot Tying'), ('Archery'), ('Kayaking')
) AS Source (SkillName)
ON 1 = 0
WHEN NOT MATCHED THEN
    INSERT (SkillName) VALUES (Source.SkillName)
    OUTPUT inserted.SkillId, inserted.SkillName INTO @SkillIdMap (SkillId, SkillName);

-- =========================================================================
-- 4. SEED SCOUT_SKILL (Assign 2 random skills to every scout)
-- =========================================================================
INSERT INTO SCOUT_SKILL (UserId, SkillId, SkillMastery)
SELECT s.UserId, k.SkillId, (ABS(CHECKSUM(NEWID())) % 5) + 1
FROM @ScoutIdMap s
CROSS APPLY (
    SELECT TOP 2 SkillId FROM @SkillIdMap ORDER BY NEWID()
) k;

-- =========================================================================
-- 5. INSERT 8 ACTIVITIES
-- =========================================================================
MERGE INTO ACTIVITY AS Target
USING (VALUES
    ('Horse Riding', 'Trail riding and equestrian safety.'),
    ('Bonfire', 'Building structural fires and campfire cooking.'),
    ('Woodcutting', 'Axe and saw safety training.'),
    ('First Aid Training', 'Emergency wilderness treatments.'),
    ('Orienteering Course', 'Compass navigation through the wilderness.'),
    ('Pioneering & Knots', 'Building structures using structural rope lashings.'),
    ('Archery Range', 'Target practice and stance training.'),
    ('Kayaking Expedition', 'River navigation and safety strokes.')
) AS Source (ActivityName, [Description])
ON 1 = 0
WHEN NOT MATCHED THEN
    INSERT (ActivityName, [Description]) VALUES (Source.ActivityName, Source.[Description])
    OUTPUT inserted.ActivityId, inserted.ActivityName INTO @ActivityIdMap (ActivityId, ActivityName);

-- =========================================================================
-- 6. INSERT ACTIVITY SCHEDULES
-- =========================================================================
MERGE INTO ACTIVITY_SCHEDULE AS Target
USING (
    SELECT ActivityId, ActivityName,
           DATETIMEFROMPARTS(2026, 11, 7, (9 + (ActivityId % 4) * 2), 0, 0, 0) AS StartTime
    FROM @ActivityIdMap
) AS Source
ON 1 = 0
WHEN NOT MATCHED THEN
    INSERT (ActivityId, [Start], [End])
    VALUES (Source.ActivityId, Source.StartTime, DATEADD(HOUR, 2, Source.StartTime))
    OUTPUT inserted.ScheduleId, Source.ActivityName INTO @ScheduleIdMap (ScheduleId, ActivityName);

-- =========================================================================
-- 7. ROSTER ATTENDEES (SCOUT_ACTIVITY)
-- =========================================================================
-- PART A: Match Scouts to activities based on their skills
INSERT INTO SCOUT_ACTIVITY (UserId, ScheduleId)
SELECT DISTINCT ss.UserId, sch.ScheduleId
FROM SCOUT_SKILL ss
JOIN @SkillIdMap sk ON ss.SkillId = sk.SkillId
JOIN @ScheduleIdMap sch ON (
    sch.ActivityName LIKE '%' + sk.SkillName + '%'
    OR (sk.SkillName = 'Fire Security' AND sch.ActivityName = 'Bonfire')
    OR (sk.SkillName = 'Knot Tying' AND sch.ActivityName = 'Pioneering & Knots')
);

-- PART B: Assign EXACTLY ONE Leader per scheduled Activity
-- Uses basic modulo math to distribute our 3 leaders evenly across the 8 sessions.
INSERT INTO SCOUT_ACTIVITY (UserId, ScheduleId)
SELECT 
    l.UserId,
    sch.ScheduleId
FROM @ScheduleIdMap sch
JOIN @LeaderIdMap l ON l.RowNum = ((sch.RowNum - 1) % 3) + 1;

SET NOCOUNT OFF;

-- =========================================================================
-- VALIDATION DASHBOARD: Show assigned supervisor and active roster
-- =========================================================================
SELECT 
    a.ActivityName,
    MAX(CASE WHEN u.IsLeader = 1 THEN u.FirstName + ' ' + u.LastName END) AS AssignedLeader,
    COUNT(CASE WHEN u.IsLeader = 0 THEN 1 END) AS AttendingScouts
FROM SCOUT_ACTIVITY sa
JOIN ACTIVITY_SCHEDULE sch ON sa.ScheduleId = sch.ScheduleId
JOIN ACTIVITY a ON sch.ActivityId = a.ActivityId
JOIN [USER] u ON sa.UserId = u.UserId
GROUP BY a.ActivityName, sch.ScheduleId
ORDER BY a.ActivityName;
