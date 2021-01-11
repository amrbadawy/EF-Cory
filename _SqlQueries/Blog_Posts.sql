SELECT * FROM [AMR].[Blogs]
SELECT * FROM [AMR].[Posts]
SELECT * FROM [AMR].[Tags]
--SELECT * FROM [AMR].[PostTags]
SELECT * FROM [AMR].[PostTags] WHERE PostId = 1 Order by CreatedAt

--DELETE FROM [AMR].[PostTags] where PostId = 1 and TagId = 103