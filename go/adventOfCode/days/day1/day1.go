package days

import (
	utils "adventOfCode/utils"
	"bufio"
	"fmt"
	"log"
	"sort"
	"strconv"
)

func PartOne() {
	file, err := utils.ReadInput("days/day1/input.txt")
	if err != nil {
		log.Fatal(err)
	}

	defer file.Close()

	current, max := 0, 0
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()
		if line == "" {
			if current > max {
				max = current
			}
			current = 0
			continue
		}

		value, err := strconv.Atoi(line)
		if err != nil {
			log.Fatal(err)
		}
		current += value
	}

	fmt.Println("Part One:", max)
}

func PartTwo() {
	file, err := utils.ReadInput("days/day1/input.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	current := 0
	elves := make([]int, 0)

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()

		if line == "" {
			elves = append(elves, current)
			current = 0
			continue
		}

		value, err := strconv.Atoi(line)
		if err != nil {
			continue
		}
		current += value
	}

	if current != 0 {
		elves = append(elves, current)
		current = 0
	}

	sort.Sort(sort.Reverse(sort.IntSlice(elves)))
	busyElves := elves[:3]

	sum := 0
	for i := 0; i < len(busyElves); i++ {
		sum += busyElves[i]
	}

	fmt.Println("Part Two:", sum)
}
