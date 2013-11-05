CREATE TABLE [dbo].[InsurancePolicies]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PolicyName] VARCHAR(50) NOT NULL, 
    [BasePrice] MONEY NOT NULL, 
    [GenderSpecific] CHAR NULL, 
    [MaximumAge] INT NULL, 
    [PolicyType] VARCHAR(50) NULL
)
