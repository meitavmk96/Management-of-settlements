-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<meitav>
-- Create date: <05/02/24>
-- Description:	<Insert a settlements>
-- =============================================
CREATE PROCEDURE spInsertSettlement
	-- Add the parameters for the stored procedure here
	@name nvarchar (30),
	@insertedId int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO [Settlements] ([name]) VALUES (@name)
	 SET @insertedId = SCOPE_IDENTITY()
END
GO

--exec spInsertSettlement N'רעננה'
--Select * from [Settlements]
--DROP PROCEDURE spInsertSettlement;
