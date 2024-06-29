CREATE VIEW [dbo].[MyMeetupsView]
AS
SELECT m.Id AS Id, m.Title AS Title, m.Location AS Location, m.Date AS Date, ma.AttendeeId AS AttendeeId
FROM dbo.Meetups AS m 
INNER JOIN dbo.MeetupAttendees AS ma ON m.Id = ma.MeetupId
GO