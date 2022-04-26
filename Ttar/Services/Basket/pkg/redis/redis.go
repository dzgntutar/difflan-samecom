package redis

import (
	"log"

	"github.com/go-redis/redis"
)

type RedisConfig struct {
	client *redis.Client
}

func NewRedisClient(Address string, Password string, db int) *RedisConfig {
	return &RedisConfig{
		client: redis.NewClient(&redis.Options{Addr: Address, Password: Password, DB: db}),
	}
}

func (r RedisConfig) Ping() string {
	value, err := r.client.Ping().Result()

	if err != nil {
		log.Fatal(err)
	}

	return value
}

func (r RedisConfig) GetStringValue(key string) (string, error) {
	if value, err := r.client.Get(key).Result(); err != nil {
		return "", err
	} else {
		return value, nil
	}
}

func (r RedisConfig) SetStringValue(key string, value string) {
	if err := r.client.Set(key, value, 0).Err(); err != nil {
		log.Fatal(err)
	}
}
