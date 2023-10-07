package utils

import "os"

func ReadInput(path string) (*os.File, error) {
	file, err := os.Open(path)
	return file, err
}
