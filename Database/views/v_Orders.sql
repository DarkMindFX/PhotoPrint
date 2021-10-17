
if exists(select 1 from sys.views where name='v_Orders' and type='v')
drop view dbo.v_Orders;
go

CREATE VIEW dbo.v_Orders
AS
SELECT
	o.ID								as [OrderID],
	manager.[Login]						as [Manager],
	client.[Login]						as [Client],
	status.[OrderStatusName]			as [Order Status],
	ds.[DeliveryServiceName]			as [Delivery Service],
	pm.[Name]							as [Payment Method],
	opd.[PaymentTransUID]				as [Transaction ID],
	o.*
	
FROM
	dbo.[Order] o
INNER JOIN dbo.[User] client		ON client.ID = o.UserID
LEFT JOIN dbo.[User] manager		ON manager.ID = o.ManagerID
INNER JOIN 
(
SELECT 
	tracking.OrderID,
	tracking.OrderStatusID,
	tracking.SetDate
FROM
	dbo.[OrderTracking] tracking
WHERE tracking.SetDate IN (SELECT MAX(SetDate) FROM dbo.[OrderTracking] GROUP BY OrderID)
)
ot	ON ot.OrderID = o.ID
INNER JOIN dbo.[OrderStatus] status ON status.ID = ot.OrderStatusID
INNER JOIN dbo.[DeliveryService] ds ON ds.ID = o.DeliveryServiceID
INNER JOIN dbo.[OrderPaymentDetails] opd ON opd.OrderID = o.ID
INNER JOIN dbo.[PaymentMethod] pm		ON pm.ID = opd.PaymentMethodID

/*
SELECT
*
FROM
dbo.v_Orders
*/