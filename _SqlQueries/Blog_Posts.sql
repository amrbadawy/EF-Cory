SELECT * FROM [BLOG].[Blogs]
SELECT * FROM [BLOG].[Posts]
SELECT * FROM [BLOG].[Tags]
--SELECT * FROM [BLOG].[PostTags]
SELECT * FROM [BLOG].[PostTags] WHERE PostId = 1 Order by CreatedAt

--DELETE FROM [BLOG].[PostTags] where PostId = 1 and TagId = 103