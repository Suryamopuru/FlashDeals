A global E-commerce website named Topkart uses an API to handle lightning deals. Lightning deals are products that are available at a discounted price on the website for a brief amount of time. The expiry time of a lightning deal is not more than 12 hours. These will be refreshed at 00:00 UTC daily. The functionality of Topkart API:
Admin actions 
Create and update lightning deals
Approve orders
Customer actions
Access available unexpired deals
Place orders 
Check the status of their order

A lightning deal contains the following data points:
Product Name
Actual & Final price
Total & Available units
Expiry Time


Built an API to handle Topkart’s lightning deals.

Considerations:
Users should not be able to place an order for a deal that is expired
