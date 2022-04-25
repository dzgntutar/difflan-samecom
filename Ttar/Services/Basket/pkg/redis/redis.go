package redis

import (
	"fmt"
	"log"

	"github.com/go-redis/redis"
)

type redisConfig struct {
	client *redis.Client
}

func NewRedisClient(Address string, Password string, db int) redisConfig {
	return redisConfig{
		client: redis.NewClient(&redis.Options{Addr: Address, Password: Password, DB: db}),
	}
}

func (r redisConfig) Ping() {
	value, err := r.client.Ping().Result()

	if err != nil {
		log.Fatal(err)
	}

	fmt.Println(value)
}

func (r redisConfig) GetStringValue(key string) (string, error) {
	if value, err := r.client.Get(key).Result(); err != nil {
		return "", err
	} else {
		return value, nil
	}
}

func (r redisConfig) SetStringValue(key string, value string) {
	if err := r.client.Set(key, value, 0).Err(); err != nil {
		log.Fatal(err)
	}
}
