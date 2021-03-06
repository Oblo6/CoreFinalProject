SET IDENTITY_INSERT [dbo].[Collectives] ON 
INSERT [dbo].[Collectives] ([Id], [Name], [CreatedDate], [CreatedBy], [Guid]) VALUES (1, N'Ev (mayıs-haziran)', CAST(N'2019-03-18T00:00:00.0000000' AS DateTime2), N'hasan', NULL)
INSERT [dbo].[Collectives] ([Id], [Name], [CreatedDate], [CreatedBy], [Guid]) VALUES (2, N'Tatil', CAST(N'2021-07-05T00:00:00.0000000' AS DateTime2), N'buse', NULL)
INSERT [dbo].[Collectives] ([Id], [Name], [CreatedDate], [CreatedBy], [Guid]) VALUES (14, N'Kedi giderleri', CAST(N'2021-07-03T22:16:26.8116891' AS DateTime2), N'buse', N'145f3844-4884-47f4-8bab-b6f3963ee10c')
INSERT [dbo].[Collectives] ([Id], [Name], [CreatedDate], [CreatedBy], [Guid]) VALUES (15, N'Kamp', CAST(N'2021-07-03T22:20:10.0974223' AS DateTime2), N'emrah', N'3b137337-dc45-4a19-b9fc-ca93a5b0304a')
INSERT [dbo].[Collectives] ([Id], [Name], [CreatedDate], [CreatedBy], [Guid]) VALUES (16, N'Ev (Temmuz)', CAST(N'2021-07-03T22:27:53.4002840' AS DateTime2), N'hasan', N'1efec1be-20a1-46f5-844c-b49346da6891')
INSERT [dbo].[Collectives] ([Id], [Name], [CreatedDate], [CreatedBy], [Guid]) VALUES (17, N'eywanın doğum günü', CAST(N'2021-07-03T22:29:31.4380138' AS DateTime2), N'buse', N'4015c02e-f5af-4b6d-aad7-44f37e440d0c')
SET IDENTITY_INSERT [dbo].[Collectives] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
INSERT [dbo].[Roles] ([Id], [Name], [Guid]) VALUES (1, N'Admin', NULL)
INSERT [dbo].[Roles] ([Id], [Name], [Guid]) VALUES (2, N'User', NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 
INSERT [dbo].[Countries] ([Id], [Name], [Guid]) VALUES (1, N'Turkey', NULL)
INSERT [dbo].[Countries] ([Id], [Name], [Guid]) VALUES (2, N'USA', NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 
INSERT [dbo].[Cities] ([Id], [Name], [CountryId], [Guid]) VALUES (1, N'Ankara', 1, NULL)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId], [Guid]) VALUES (2, N'Istanbul', 1, NULL)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId], [Guid]) VALUES (3, N'New York', 2, NULL)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId], [Guid]) VALUES (4, N'İzmir', 1, N'44187784-4580-483d-b51b-78a6c826544a')
INSERT [dbo].[Cities] ([Id], [Name], [CountryId], [Guid]) VALUES (5, N'Malatya', 1, N'9e4ba79b-1f75-4963-8aa6-4963ee9b9e7a')
INSERT [dbo].[Cities] ([Id], [Name], [CountryId], [Guid]) VALUES (6, N'Kayseri', 1, N'd620eccf-aa66-4607-b522-c6c1596b8d77')
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[UserDetails] ON 
INSERT [dbo].[UserDetails] ([Id], [EMail], [CountryId], [CityId], [Address], [Guid]) VALUES (2, N'oblo@coal.com', 1, 1, N'Sokak', NULL)
INSERT [dbo].[UserDetails] ([Id], [EMail], [CountryId], [CityId], [Address], [Guid]) VALUES (3, N'buse@coal.com', 1, 5, N'blabla', NULL)
INSERT [dbo].[UserDetails] ([Id], [EMail], [CountryId], [CityId], [Address], [Guid]) VALUES (4, N'emrah@coal.com', 1, 6, N'street', NULL)
INSERT [dbo].[UserDetails] ([Id], [EMail], [CountryId], [CityId], [Address], [Guid]) VALUES (5, N'kekik@coal.com', 1, 1, N'blabla', NULL)
INSERT [dbo].[UserDetails] ([Id], [EMail], [CountryId], [CityId], [Address], [Guid]) VALUES (6, N'eywa@coal.com', 1, 1, N'Demirtepe', NULL)
INSERT [dbo].[UserDetails] ([Id], [EMail], [CountryId], [CityId], [Address], [Guid]) VALUES (7, N'ezgi@coal.com', 1, 1, N'bglr', NULL)
SET IDENTITY_INSERT [dbo].[UserDetails] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Password], [Active], [RoleId], [UserDetailId], [Guid]) VALUES (2, N'hasan', N'123', 1, 1, 2, NULL)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [Active], [RoleId], [UserDetailId], [Guid]) VALUES (3, N'buse', N'buse', 1, 2, 3, NULL)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [Active], [RoleId], [UserDetailId], [Guid]) VALUES (4, N'emrah', N'emrah', 1, 2, 4, NULL)
INSERT [dbo].[Users] ([Id], [UserName], [Password], [Active], [RoleId], [UserDetailId], [Guid]) VALUES (5, N'kekik', N'kekik', 0, 2, 5, N'7019f93a-0960-462b-ad67-64872dbe82e8')
INSERT [dbo].[Users] ([Id], [UserName], [Password], [Active], [RoleId], [UserDetailId], [Guid]) VALUES (6, N'eywa', N'eywa', 0, 2, 6, N'2e4f52b7-5871-4ad8-9369-1170e6be27dc')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[CollectiveUser] ON 
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (1, 2, 1, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (2, 3, 1, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (3, 4, 1, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (4, 2, 2, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (5, 3, 2, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (62, 4, 14, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (65, 3, 14, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (66, 2, 15, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (67, 3, 15, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (68, 4, 15, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (69, 3, 16, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (70, 4, 16, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (71, 2, 16, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (72, 2, 17, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (73, 4, 17, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (74, 5, 17, NULL)
INSERT [dbo].[CollectiveUser] ([Id], [UserId], [CollectiveId], [Guid]) VALUES (76, 3, 17, NULL)
SET IDENTITY_INSERT [dbo].[CollectiveUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Expenses] ON 
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (1, N'Kira', 1200, CAST(N'2021-05-18T00:00:00.0000000' AS DateTime2), 1, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (3, N'Fatura SU', 80, CAST(N'2021-05-19T00:00:00.0000000' AS DateTime2), 3, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (4, N'Fatura Elk', 100, CAST(N'2021-05-20T00:00:00.0000000' AS DateTime2), 2, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (6, N'Manav', 50, CAST(N'2021-05-21T00:00:00.0000000' AS DateTime2), 2, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (7, N'biletler', 200, CAST(N'2021-06-03T00:00:00.0000000' AS DateTime2), 5, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (8, N'otel', 1600, CAST(N'2021-03-10T00:00:00.0000000' AS DateTime2), 4, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (9, N'kano', 150, CAST(N'2021-03-01T00:00:00.0000000' AS DateTime2), 5, NULL)
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (11, N'su', 50, CAST(N'2021-06-03T22:04:34.9549217' AS DateTime2), 1, N'668aa234-77e0-4068-82ea-f3edc736708a')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (14, N'Terlik', 100, CAST(N'2021-06-04T16:42:22.6224371' AS DateTime2), 2, N'7af71dfb-09dc-4e59-8994-6a35ef6f2bd1')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (16, N'Tadilat', 200, CAST(N'2021-06-09T19:20:39.4984196' AS DateTime2), 1, N'3379d153-cece-46cc-ac02-3a907d4fa3e7')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (17, N'su', 20, CAST(N'2021-06-09T19:56:52.0271489' AS DateTime2), 4, N'65467289-14b4-481b-add9-30b37f99748a')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (38, N'15Kg Mama', 650, CAST(N'2021-07-03T22:16:43.7849213' AS DateTime2), 65, N'e998e639-50ab-4e57-8da1-da32ef5767c1')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (39, N'20lt kum', 45, CAST(N'2021-07-03T22:17:00.2562648' AS DateTime2), 65, N'ea71f325-735c-4562-9399-620f7ca4ee41')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (40, N'Yaş mama', 50, CAST(N'2021-07-03T22:17:29.6147059' AS DateTime2), 65, N'a151db8d-1b2a-441e-8de8-e3171dd12c30')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (41, N'Malt Macun', 60, CAST(N'2021-07-03T22:17:39.0378215' AS DateTime2), 65, N'385f2a7f-be65-4314-be6d-0643203a17b0')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (42, N'Tarak', 40, CAST(N'2021-07-03T22:18:02.9425753' AS DateTime2), 65, N'fe43f02e-a12e-472e-93d7-b2df7ddeb4cd')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (43, N'Ödül çubuğu', 30, CAST(N'2021-07-03T22:18:23.6160474' AS DateTime2), 65, N'3344eefe-2ce2-441c-aca0-a8bf2090c2fc')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (44, N'10kg mama', 450, CAST(N'2021-07-03T22:18:54.6141761' AS DateTime2), 62, N'37e80483-20cc-4a4e-809a-8d1da5096cb0')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (45, N'Veteriner', 240, CAST(N'2021-07-03T22:19:10.6774683' AS DateTime2), 62, N'20f923f4-8f79-4efa-812a-a82cd0ae265e')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (46, N'Yakıt', 150, CAST(N'2021-07-03T22:20:21.3349734' AS DateTime2), 68, N'b3ce7720-3e08-4587-8821-16cb340d0c49')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (47, N'Market', 230, CAST(N'2021-07-03T22:20:35.6333649' AS DateTime2), 68, N'4e287fce-04e6-4e4a-885c-19ecf02384b2')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (48, N'İçkiler', 300, CAST(N'2021-07-03T22:21:07.4711732' AS DateTime2), 66, N'7efba591-f27b-4c02-b5c2-1de5d4325916')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (49, N'diğer', 1000, CAST(N'2021-07-03T22:21:47.9997260' AS DateTime2), 67, N'8167e6a3-930f-4921-b7dd-937b33989b2a')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (50, N'Kira', 1200, CAST(N'2021-07-03T22:28:02.9349943' AS DateTime2), 71, N'a8be5ffd-085f-47e4-87d9-b832d0cc311b')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (51, N'elk', 130, CAST(N'2021-07-03T22:28:24.1384634' AS DateTime2), 69, N'919fe796-170c-47fb-ad49-7e0bf68147cd')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (52, N'su', 100, CAST(N'2021-07-03T22:28:30.5596912' AS DateTime2), 69, N'871946a8-9c2d-46e6-b3c5-bd19e786d5f0')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (53, N'doğalgaz', 20, CAST(N'2021-07-03T22:28:40.2686369' AS DateTime2), 69, N'55ff3cce-909d-40a1-9d88-c07874b47d3a')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (54, N'int', 90, CAST(N'2021-07-03T22:28:47.9450041' AS DateTime2), 69, N'7adcbfa4-980d-4f04-a673-df971a1c6671')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (55, N'Aidat', 35, CAST(N'2021-07-03T22:29:10.3662358' AS DateTime2), 69, N'95557e81-771e-4bb4-a969-193b01d54aa7')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (56, N'Pasta', 80, CAST(N'2021-07-03T22:29:47.1391361' AS DateTime2), 76, N'422dddfa-50fa-4694-ac26-056d00746a39')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (57, N'eywaya yaş mama', 20, CAST(N'2021-07-03T22:29:59.9400770' AS DateTime2), 76, N'394e988a-bc42-4382-b996-6b6c2690e3c2')
INSERT [dbo].[Expenses] ([Id], [Description], [Cost], [PayDate], [CollectiveUserId], [Guid]) VALUES (58, N'içkiler', 150, CAST(N'2021-07-03T22:30:11.1090387' AS DateTime2), 76, N'41b9f266-3fd5-4c70-b2e7-9eb18c80bf60')
SET IDENTITY_INSERT [dbo].[Expenses] OFF
GO
