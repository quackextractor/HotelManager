USE [HotelDB]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 19/02/2025 22:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order]
(
    [id]              [int] IDENTITY (1,1) NOT NULL,
    [price_per_night] [float]              NOT NULL,
    [nights]          [int]                NOT NULL,
    [order_date]      [datetime2](7)       NOT NULL,
    [checkin_date]    [date]               NOT NULL,
    [status]          [nvarchar](30)       NOT NULL,
    [paid]            [bit]                NOT NULL,
    [total_price]     AS ([price_per_night] * [nights]) PERSISTED NOT NULL,
    [room_id]         [int]                NULL,
    PRIMARY KEY CLUSTERED
        (
         [id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderRole]    Script Date: 19/02/2025 22:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderRole]
(
    [id]        [int] IDENTITY (1,1) NOT NULL,
    [person_id] [int]                NOT NULL,
    [order_id]  [int]                NOT NULL,
    [role]      [nvarchar](20)       NOT NULL,
    PRIMARY KEY CLUSTERED
        (
         [id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    CONSTRAINT [UQ_OrderRole] UNIQUE NONCLUSTERED
        (
         [person_id] ASC,
         [order_id] ASC,
         [role] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 19/02/2025 22:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment]
(
    [id]             [int] IDENTITY (1,1) NOT NULL,
    [order_id]       [int]                NOT NULL,
    [amount]         [float]              NOT NULL,
    [payment_date]   [datetime2](7)       NOT NULL,
    [payment_method] [nvarchar](20)       NOT NULL,
    [note]           [nvarchar](max)      NULL,
    PRIMARY KEY CLUSTERED
        (
         [id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 19/02/2025 22:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person]
(
    [id]                [int] IDENTITY (1,1) NOT NULL,
    [first_name]        [nvarchar](50)       NOT NULL,
    [last_name]         [nvarchar](50)       NOT NULL,
    [email]             [nvarchar](255)      NOT NULL,
    [phone]             [nvarchar](20)       NULL,
    [status]            [nvarchar](20)       NOT NULL,
    [registration_date] [datetime2](7)       NOT NULL,
    [last_visit_date]   [datetime2](7)       NULL,
    PRIMARY KEY CLUSTERED
        (
         [id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    UNIQUE NONCLUSTERED
        (
         [email] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 19/02/2025 22:46:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room]
(
    [id]          [int] IDENTITY (1,1) NOT NULL,
    [room_number] [nvarchar](10)       NOT NULL,
    [room_type]   [nvarchar](20)       NOT NULL,
    [capacity]    [int]                NOT NULL,
    [price]       [float]              NOT NULL,
    PRIMARY KEY CLUSTERED
        (
         [id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    UNIQUE NONCLUSTERED
        (
         [room_number] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]
    ADD DEFAULT (getdate()) FOR [order_date]
GO
ALTER TABLE [dbo].[Order]
    ADD DEFAULT ((0)) FOR [paid]
GO
ALTER TABLE [dbo].[Payment]
    ADD DEFAULT (getdate()) FOR [payment_date]
GO
ALTER TABLE [dbo].[Person]
    ADD DEFAULT (getdate()) FOR [registration_date]
GO
ALTER TABLE [dbo].[Order]
    WITH CHECK ADD CONSTRAINT [FK_Order_Room] FOREIGN KEY ([room_id])
        REFERENCES [dbo].[Room] ([id])
GO
ALTER TABLE [dbo].[Order]
    CHECK CONSTRAINT [FK_Order_Room]
GO
ALTER TABLE [dbo].[OrderRole]
    WITH CHECK ADD CONSTRAINT [FK_OrderRole_Order] FOREIGN KEY ([order_id])
        REFERENCES [dbo].[Order] ([id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderRole]
    CHECK CONSTRAINT [FK_OrderRole_Order]
GO
ALTER TABLE [dbo].[OrderRole]
    WITH CHECK ADD CONSTRAINT [FK_OrderRole_Person] FOREIGN KEY ([person_id])
        REFERENCES [dbo].[Person] ([id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderRole]
    CHECK CONSTRAINT [FK_OrderRole_Person]
GO
ALTER TABLE [dbo].[Payment]
    WITH CHECK ADD CONSTRAINT [FK_Payment_Order] FOREIGN KEY ([order_id])
        REFERENCES [dbo].[Order] ([id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payment]
    CHECK CONSTRAINT [FK_Payment_Order]
GO
ALTER TABLE [dbo].[Order]
    WITH CHECK ADD CHECK (([nights] > (0)))
GO
ALTER TABLE [dbo].[Order]
    WITH CHECK ADD CHECK (([price_per_night] >= (0)))
GO
ALTER TABLE [dbo].[Payment]
    WITH CHECK ADD CHECK (([amount] > (0)))
GO
ALTER TABLE [dbo].[Room]
    WITH CHECK ADD CHECK (([capacity] > (0)))
GO
ALTER TABLE [dbo].[Room]
    WITH CHECK ADD CHECK (([price] >= (0)))
GO
