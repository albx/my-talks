SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AttendToMeetupStoredProcedure]
	@attendeeId nvarchar(255),
	@attendeeName nvarchar(255),
	@meetupId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @existAlready int;
	SET @existAlready = (SELECT COUNT(Id) FROM [dbo].[MeetupAttendees] WHERE MeetupId=@meetupId AND AttendeeId=@attendeeId)

	IF @existAlready = 0
	BEGIN
		INSERT INTO [dbo].[MeetupAttendees](Id, MeetupId, AttendeeId, AttendeeName) VALUES(NEWID(), @meetupId, @attendeeId, @attendeeName)
	END
END
GO