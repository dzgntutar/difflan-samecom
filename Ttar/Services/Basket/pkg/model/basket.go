package model

type Basket struct {
	UserId       string
	DiscountCode string
	DiscourRate  int
	BasketItem   []BasketItem
	TotalPrice   float32
}

type BasketItem struct {
	Quantity    int
	ProductId   string
	ProductName string
	Price       float32
}
