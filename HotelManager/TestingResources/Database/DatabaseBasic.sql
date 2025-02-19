create table Person
(
    id                int identity
        primary key,
    first_name        nvarchar(50)                not null,
    last_name         nvarchar(50)                not null,
    email             nvarchar(255)               not null
        unique,
    phone             nvarchar(20),
    status            nvarchar(20)                not null,
    registration_date datetime2 default getdate() not null,
    last_visit_date   datetime2
)
go

create table Room
(
    id          int identity
        primary key,
    room_number nvarchar(10) not null
        unique,
    room_type   nvarchar(20) not null,
    capacity    int          not null
        check ([capacity] > 0),
    price       float        not null
        check ([price] >= 0)
)
go

create table [Order]
(
    id              int identity
        primary key,
    price_per_night float                       not null
        check ([price_per_night] >= 0),
    nights          int                         not null
        check ([nights] > 0),
    order_date      datetime2 default getdate() not null,
    checkin_date    date                        not null,
    status          nvarchar(30)                not null,
    paid            bit       default 0         not null,
    total_price     as [price_per_night] * [nights],
    room_id         int
        constraint FK_Order_Room
            references Room
)
go

create table OrderRole
(
    id        int identity
        primary key,
    person_id int          not null
        constraint FK_OrderRole_Person
            references Person
            on delete cascade,
    order_id  int          not null
        constraint FK_OrderRole_Order
            references [Order]
            on delete cascade,
    role      nvarchar(20) not null,
    constraint UQ_OrderRole
        unique (person_id, order_id, role)
)
go

create table Payment
(
    id             int identity
        primary key,
    order_id       int                         not null
        constraint FK_Payment_Order
            references [Order]
            on delete cascade,
    amount         float                       not null
        check ([amount] > 0),
    payment_date   datetime2 default getdate() not null,
    payment_method nvarchar(20)                not null,
    note           nvarchar(max)
)
go