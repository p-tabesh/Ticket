/*
-- Delete all data from tables before inserting new data
ALTER TABLE TicketStatusHistory NOCHECK CONSTRAINT ALL;
ALTER TABLE TicketNote NOCHECK CONSTRAINT ALL;
ALTER TABLE TicketAudit NOCHECK CONSTRAINT ALL;
ALTER TABLE Tickets NOCHECK CONSTRAINT ALL;
ALTER TABLE CategoryField NOCHECK CONSTRAINT ALL;
ALTER TABLE Field NOCHECK CONSTRAINT ALL;
ALTER TABLE Category NOCHECK CONSTRAINT ALL;
ALTER TABLE Users NOCHECK CONSTRAINT ALL;
ALTER TABLE Team NOCHECK CONSTRAINT ALL;

-- Now delete the data
DELETE FROM TicketStatusHistory;
DELETE FROM TicketNote;
DELETE FROM TicketAudit;
DELETE FROM Tickets;
DELETE FROM CategoryField;
DELETE FROM Field;
DELETE FROM Category;
DELETE FROM Users;
DELETE FROM Team;

-- Re-enable constraints
ALTER TABLE TicketStatusHistory WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE TicketNote WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE TicketAudit WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE Tickets WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE CategoryField WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE Field WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE Category WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE Users WITH CHECK CHECK CONSTRAINT ALL;
ALTER TABLE Team WITH CHECK CHECK CONSTRAINT ALL;
*/

-- Insert new data
SET IDENTITY_INSERT Team ON;
INSERT INTO Team (id, title)
VALUES (1, 'Support Team'), (2, 'IT Team'), (3, 'Development Team'), (4, 'Customer Service'), (5, 'QA Team');
SET IDENTITY_INSERT Team OFF;

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (id, username, password, email, teamid, isactive, creationdate)
VALUES 
(1, 'john_doe', 'hashed_password1', 'john@example.com', 1, 1, GETDATE()),
(2, 'jane_smith', 'hashed_password2', 'jane@example.com', 2, 1, GETDATE()),
(3, 'mike_jones', 'hashed_password3', 'mike@example.com', 3, 1, GETDATE()),
(4, 'susan_lee', 'hashed_password4', 'susan@example.com', 4, 1, GETDATE()),
(5, 'david_kim', 'hashed_password5', 'david@example.com', 5, 1, GETDATE());
SET IDENTITY_INSERT Users OFF;

SET IDENTITY_INSERT Category ON;
INSERT INTO Category (id, title, parentid, DefaultUserAsignId)
VALUES 
(1, 'Technical Issues', NULL, 1),
(2, 'Billing Issues', NULL, 2),
(3, 'Software Bug', 1, 1),
(4, 'Hardware Failure', 1, 3),
(5, 'Login Issues', NULL, 4);
SET IDENTITY_INSERT Category OFF;
delete from Category

SET IDENTITY_INSERT Field ON;
INSERT INTO Field (id, [name], [Type], isrequired)
VALUES 
(1, 'Issue Type', 0, 1),
(2, 'Description', 1, 1),
(3, 'Urgency Level', 0, 1),
(4, 'Operating System', 1, 0);
SET IDENTITY_INSERT Field OFF;

INSERT INTO CategoryField (categoryid, fieldid)
VALUES (1, 1), (1, 2), (2, 1), (3, 3), (4, 4);

SET IDENTITY_INSERT Tickets ON;
INSERT INTO Tickets (id, subject, body, responsebody, status, priority, nationalcode, phonenumber, creationdate, categoryid, userid, assignuserid)
VALUES 
(1, 'System Crash', 'My system keeps crashing', NULL, 0, 0, '1234567890', '9876543210', GETDATE(), 1, 1, 2),
(2, 'Payment Not Processed', 'I was charged but my payment is not reflecting.', NULL, 1, 1, '2234567890', '9876543220', GETDATE(), 2, 2, 1),
(3, 'Blue Screen Error', 'My computer shows a blue screen.', NULL, 0, 2, '3234567890', '9876543230', GETDATE(), 3, 3, 1),
(4, 'Forgot Password', 'I cannot reset my password.', NULL, 1, 1, '4234567890', '9876543240', GETDATE(), 5, 4, 2),
(5, 'Slow Performance', 'My application runs very slow.', NULL, 0, 2, '5234567890', '9876543250', GETDATE(), 3, 5, 3);
SET IDENTITY_INSERT Tickets OFF;

SET IDENTITY_INSERT TicketAudit ON;
INSERT INTO TicketAudit (id, action, description, creationdate, ticketid, userid)
VALUES 
(1, 0, 'Ticket Created', GETDATE(), 1, 1),
(2, 1, 'Ticket Updated', GETDATE(), 2, 2),
(3, 0, 'Ticket Created', GETDATE(), 3, 3),
(4, 1, 'Password reset requested', GETDATE(), 4, 4),
(5, 0, 'Performance issue reported', GETDATE(), 5, 5);
SET IDENTITY_INSERT TicketAudit OFF;

SET IDENTITY_INSERT TicketNote ON;
INSERT INTO TicketNote (id, note, userid, ticketid)
VALUES 
(1, 'User reported system crash issue', 1, 1),
(2, 'Billing issue reported', 2, 2),
(3, 'User experiencing blue screen error', 3, 3),
(4, 'Password reset link sent', 4, 4),
(5, 'Performance issue under investigation', 5, 5);
SET IDENTITY_INSERT TicketNote OFF;

SET IDENTITY_INSERT TicketStatusHistory ON;
INSERT INTO TicketStatusHistory (id, status, ticketid)
VALUES 
(1, 1, 1),
(2, 2, 2),
(3, 1, 3),
(4, 3, 4),
(5, 1, 5);
SET IDENTITY_INSERT TicketStatusHistory OFF;


-- Select all data from each table
SELECT * FROM Team;
SELECT * FROM Users;
SELECT * FROM Category;
SELECT * FROM Field;
SELECT * FROM CategoryField;
SELECT * FROM Tickets;
SELECT * FROM TicketAudit;
SELECT * FROM TicketNote;
SELECT * FROM TicketStatusHistory;

