dab add Meetup --source dbo.Meetups --permissions "anonymous:read" --config .\swa-db-connections\staticwebapp.database.config.json

dab add MeetupAttendee --source dbo.MeetupAttendees --permissions "anonymous:read" --config .\swa-db-connections\staticwebapp.database.config.json

dab add MyMeetups --source dbo.MyMeetupsView --source.type View --source.key-fields "Id"  --permissions "attendee:read" --config .\swa-db-connections\staticwebapp.database.config.json

dab add AttendToMeetup --source dbo.AttendToMeetupStoredProcedure --source.type "stored-procedure" --source.params "attendeeId:attendeeId,attendeeName:attendeeName,meetupId:meetupId" --permissions "authenticated:execute" --rest.methods "post" --graphql false --config .\swa-db-connections\staticwebapp.database.config.json