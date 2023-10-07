package days

import (
	utils "adventOfCode/utils"
	"bufio"
	"fmt"
	"log"
	"strings"
)

func PartOne() {
	file, err := utils.ReadInput("days/day2/input.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	totalScore := 0
	for scanner.Scan() {
		line := scanner.Text()
		score := calculateRoundScore(line)
		totalScore += score
	}

	fmt.Println("Part One:", totalScore)
}

func PartTwo() {
	file, err := utils.ReadInput("days/day2/input.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()
}

func getShapeToPlay(line string) rune {
	input := strings.Replace(line, " ", "", -1)
	x := rune(input[0])
	y := rune(input[1])

	shapeToPlay rune = nil
	if y == 'X' {
		if x == 'A' {
			shapeToPlay =

		} else if x == 'B' {

		} else if x == 'C' {

		}
	} else if y == 'Y' {
		if x == 'A' {

		} else if x == 'B' {

		} else if x == 'C' {

		}
	} else if y == 'Z' {
		if x == 'A' {
		} else if x == 'B' {
		} else if x == 'C' {
		}
	}

	return shapeToPlay
}

func calculateRoundScore(line string) int {
	input := strings.Replace(line, " ", "", -1)
	x := rune(input[0])
	y := rune(input[1])

	score := 0
	a := []rune{'A', 'B', 'C'}
	b := []rune{'X', 'Y', 'Z'}

	if y == b[0] {
		score += 1

		if x == a[0] {
			score += 3
		} else if x == a[1] {
			score += 0
		} else if x == a[2] {
			score += 6
		}
	} else if y == b[1] {
		score += 2

		if x == a[0] {
			score += 6
		} else if x == a[1] {
			score += 3
		} else if x == a[2] {
			score += 0
		}
	} else if y == b[2] {
		score += 3

		if x == a[0] {
			score += 0
		} else if x == a[1] {
			score += 6
		} else if x == a[2] {
			score += 3
		}
	}

	return score
}
