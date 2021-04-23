USE [CovidApp]
GO

/****** Object:  Table [dbo].[PatientAddress]    Script Date: 23/04/2021 10:09:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Street] [varchar](300) NULL,
	[Suburb] [varchar](300) NULL,
	[State] [varchar](40) NULL,
	[PatientDetailsFK] [int] NULL,
 CONSTRAINT [PK_PatientAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PatientAddress]  WITH CHECK ADD  CONSTRAINT [FK_PatientAddress_PatientDetails1] FOREIGN KEY([PatientDetailsFK])
REFERENCES [dbo].[PatientDetails] ([Id])
GO

ALTER TABLE [dbo].[PatientAddress] CHECK CONSTRAINT [FK_PatientAddress_PatientDetails1]
GO


/****** Object:  Table [dbo].[PatientDetails]    Script Date: 23/04/2021 10:09:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](300) NULL,
	[LastName] [varchar](300) NULL,
	[Age] [int] NULL,
 CONSTRAINT [PK_PatientDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PatientHospital]    Script Date: 23/04/2021 10:09:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientHospital](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HospitalName] [varchar](400) NULL,
	[DateOfTest] [datetime] NULL,
	[HealthCarerName] [varchar](400) NULL,
	[PatientDetailsIdFk] [int] NULL,
 CONSTRAINT [PK_PatientHospital] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PatientHospital]  WITH CHECK ADD  CONSTRAINT [FK_PatientHospital_PatientDetails1] FOREIGN KEY([PatientDetailsIdFk])
REFERENCES [dbo].[PatientDetails] ([Id])
GO

ALTER TABLE [dbo].[PatientHospital] CHECK CONSTRAINT [FK_PatientHospital_PatientDetails1]
GO


/****** Object:  Table [dbo].[PatientHospitalCovidTest]    Script Date: 23/04/2021 10:09:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientHospitalCovidTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Results] [bit] NULL,
	[Notes] [varchar](100) NULL,
	[PatientHospitalIdFk] [int] NULL,
	[PatientDetailsIdFk] [int] NULL,
 CONSTRAINT [PK_PatientHospitalCovidTest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PatientHospitalCovidTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientHospitalCovidTest_PatientDetails1] FOREIGN KEY([PatientDetailsIdFk])
REFERENCES [dbo].[PatientDetails] ([Id])
GO

ALTER TABLE [dbo].[PatientHospitalCovidTest] CHECK CONSTRAINT [FK_PatientHospitalCovidTest_PatientDetails1]
GO

ALTER TABLE [dbo].[PatientHospitalCovidTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientHospitalCovidTest_PatientHospital1] FOREIGN KEY([PatientHospitalIdFk])
REFERENCES [dbo].[PatientHospital] ([Id])
GO

ALTER TABLE [dbo].[PatientHospitalCovidTest] CHECK CONSTRAINT [FK_PatientHospitalCovidTest_PatientHospital1]
GO

/****** Object:  Table [dbo].[PatientNextOfKin]    Script Date: 23/04/2021 10:10:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientNextOfKin](
	[Id] [uniqueidentifier] NOT NULL,
	[PhoneNumber] [varchar](90) NULL,
	[Name] [varchar](90) NULL,
	[Relationship] [varchar](90) NULL,
	[PatientDetailsFK] [int] NULL,
 CONSTRAINT [PK_PatientNextOfKin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PatientNextOfKin]  WITH CHECK ADD  CONSTRAINT [FK_PatientNextOfKin_PatientDetails1] FOREIGN KEY([PatientDetailsFK])
REFERENCES [dbo].[PatientDetails] ([Id])
GO

ALTER TABLE [dbo].[PatientNextOfKin] CHECK CONSTRAINT [FK_PatientNextOfKin_PatientDetails1]
GO


/****** Object:  Table [dbo].[User]    Script Date: 23/04/2021 10:10:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FullName] [varchar](90) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [CovidApp]
GO

/****** Object:  Table [dbo].[UserClaim]    Script Date: 23/04/2021 10:10:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserClaim](
	[UserClaim] [uniqueidentifier] NOT NULL,
	[UserID_FK] [uniqueidentifier] NOT NULL,
	[ClaimType] [varchar](100) NOT NULL,
	[ClaimValue] [bit] NOT NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[UserClaim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_User_UserClaim] FOREIGN KEY([UserID_FK])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_User_UserClaim]
GO


/****** Object:  StoredProcedure [dbo].[sp_InsertNewPatientDetailsAndAddress]    Script Date: 23/04/2021 10:11:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_InsertNewPatientDetailsAndAddress]
  @firstName varchar(300) = null,
  @lastName varchar(300) = null,
  @age int = null,
  @street varchar(300) = null,
  @suburb varchar(300) = null,
  @state varchar(40) = null,
  @hospitalName varchar(300) = null,
  @dateOfTest datetime,
  @healthCarerName varchar(300) = null,
  @results bit,
  @notes varchar(300) = null
As
	Set NoCount On
	Declare @patientId int, @patientHospitalIdFk int
	begin try
		Insert dbo.PatientDetails(FirstName, LastName, Age) 
		Values(@firstName, @lastName, @age)
		Set @patientId = scope_Identity()

		Insert dbo.PatientAddress(Street, Suburb, State, PatientDetailsFK)
		values(@street, @suburb, @state, @patientId)

		Insert dbo.PatientHospital(HospitalName, DateOfTest, HealthCarerName, PatientDetailsIdFk)
		Values(@hospitalName, @dateOfTest, @healthCarerName, @patientId)
		set @patientHospitalIdFk = SCOPE_IDENTITY()
		print @patientHospitalIdFk 

		Insert dbo.PatientHospitalCovidTest(Results, Notes, PatientHospitalIdFk, PatientDetailsIdFk)
		Values(@results, @notes, @patientHospitalIdFk, @patientId)

		select 'New ID of patient: ' + CAST(@patientId as varchar(10)) 
	end try
	begin catch
		SELECT dbo.sqlErrorHandler()
	end catch
 
GO

/****** Object:  StoredProcedure [dbo].[sp_UpdatePatientAddress]    Script Date: 23/04/2021 10:11:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_UpdatePatientAddress]
  @id int,
  @street varchar(300) = null,
  @suburb varchar(300) = null,
  @state varchar(40) = null
As
	Set NoCount On
	declare @test int;
	begin try
		--update dbo.PatientDetails set FirstName = @firstName, LastName = @lastName, Age = @age where id = @id 
		update dbo.PatientAddress set Street = @street, Suburb = @suburb, State = @state where id = @id
		select 'Updated patient Id: ' + CAST(@id as varchar(10)) 
	end try
	begin catch
		SELECT dbo.sqlErrorHandler()
	end catch
GO

/****** Object:  StoredProcedure [dbo].[sp_UpdatePatientNextOfKin]    Script Date: 23/04/2021 10:11:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_UpdatePatientNextOfKin]
  @id uniqueidentifier,
  @phoneNumber varchar(90) = null,
  @name varchar(90) = null,
  @relationship varchar(90) = null
As
	Set NoCount On
	begin try
		--update dbo.PatientDetails set FirstName = @firstName, LastName = @lastName, Age = @age where id = @id 
		update dbo.PatientNextOfKin set PhoneNumber = @phoneNumber, [Name] = @name, Relationship = @relationship where id = @id
		select 'Updated patient Id: ' + CAST(@id as varchar(600)) 
	end try
	begin catch
		SELECT dbo.sqlErrorHandler()
	end catch
GO

/****** Object:  UserDefinedFunction [dbo].[sqlErrorHandler]    Script Date: 23/04/2021 10:12:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[sqlErrorHandler]()
RETURNS varchar(max)
AS 
BEGIN
	declare @errorNumber int, @errorState int, @errorSeverity int, 
	@errorProcedure varchar(max), @errorLine int, @errorMessage varchar(max), @overallErrorMsg varchar(max)

	SELECT  @errorNumber = ERROR_NUMBER(), 
			@errorState = ERROR_STATE(), 
			@errorSeverity = ERROR_SEVERITY(), 
			@errorProcedure = ERROR_PROCEDURE(), 
			@errorLine = ERROR_LINE(), 
			@errorMessage = ERROR_MESSAGE()

	set @overallErrorMsg = 'SQL error has occurred, this is information about the error: ' + 
	'Error Number: ' + 	CAST(isnull(@errorNumber,0) as varchar(10)) + 
	' Error State:  ' + CAST(isnull(@errorState,0) as varchar(10)) + 
    ' Error Severity: ' + CAST(isnull(@errorSeverity,0) as varchar(10)) + 
	' Error Procedure: ' + isnull(@errorProcedure,'') + 
	' Error Line: ' +  CAST(isnull(@errorLine,0) as varchar(10))  + 
	' Error Message: ' + isnull(@errorMessage,'')
    RETURN @overallErrorMsg
END;
GO

