﻿SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE DATABASE LACES
GO

USE LACES
GO

CREATE LOGIN LACES_USER WITH PASSWORD = '5Quidt3rn'
GO

CREATE USER LACES_USER FOR LOGIN LACES_USER
GO

GRANT INSERT TO LACES_USER
GRANT SELECT TO LACES_USER
GRANT UPDATE TO LACES_USER
GRANT DELETE TO LACES_USER
GO