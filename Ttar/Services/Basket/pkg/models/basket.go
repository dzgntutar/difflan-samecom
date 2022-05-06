package models

type Basket struct {
	UserId       string
	DiscountCode string
	DiscountRate int
	BasketItem   []BasketItem
	TotalPrice   float32
}
