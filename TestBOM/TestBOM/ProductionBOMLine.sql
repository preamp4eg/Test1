CREATE TABLE [dbo].[ProductionBOMLine]
(
	[ProductionBOM_No] NVARCHAR(20) NOT NULL PRIMARY KEY NONCLUSTERED,
	[Line_No] int IDENTITY (1,1) NOT NULL , 
    [Type] NVARCHAR(20) NULL, 
    [No] NVARCHAR(20) NULL, 
    [Quantity] INT NULL, 
    CONSTRAINT [FK_ProductionBOMLine_Item] FOREIGN KEY ([No]) REFERENCES [Item]([No]), 
    CONSTRAINT [FK_ProductionBOMLine_ProductionBOMHeader_1] FOREIGN KEY ([No]) REFERENCES [ProductionBOMHeader]([No]), 
    CONSTRAINT [FK_ProductionBOMLine_ProductionBOMHeader_2] FOREIGN KEY ([ProductionBOM_No]) REFERENCES [ProductionBOMHeader]([No]), 
   
)
