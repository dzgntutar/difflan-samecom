package handlers

import (
	. "basket/pkg/model"
	"basket/pkg/redis"
	"encoding/json"
	"fmt"
	"net/http"
)

func BasketHandler(redisConfig *redis.RedisConfig) http.Handler {
	fmt.Println(redisConfig)
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		switch r.Method {
		case "GET":
			data, _ := redisConfig.GetStringValue("basket")
			w.Write([]byte(data))

		case "POST":
			basket := Basket{
				UserId:       "asdfasdf",
				DiscountCode: "12AA",
				DiscourRate:  25,
				BasketItem: []BasketItem{
					{Quantity: 12, ProductId: "asfasdf", ProductName: "name", Price: 12},
				},
			}
			
			var totalP float32
			for _, value := range basket.BasketItem {
				totalP += float32(value.Quantity) * value.Price
			}
			basket.TotalPrice = totalP

			data, _ := json.Marshal(basket)

			strData := string(data)

			redisConfig.SetStringValue("basket", strData)

			w.Write([]byte(data))

		default:
			w.WriteHeader(http.StatusNotImplemented)
			w.Write([]byte(http.StatusText(http.StatusNotImplemented)))
			w.WriteHeader(http.StatusOK)
			w.Write([]byte("Get"))
		}
	})
}
