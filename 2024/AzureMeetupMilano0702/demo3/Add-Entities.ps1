dab add Meetup --source dbo.Meetups --permissions "anonymous:read" --config .\swa-db-connections\staticwebapp.database.config.json

dab add MeetupAttendee --source dbo.MeetupAttendees --permissions "anonymous:read" --config .\swa-db-connections\staticwebapp.database.config.json