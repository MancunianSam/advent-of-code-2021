package main

import (
	"os"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

type result struct {
	position int
	number   int
	sum      int
}

func main() {
	f, err := os.ReadFile("input.txt")
	check(err)
	var fileString = string(f)
	var inputArr = strings.Split(fileString, "\n")
	var selectedNumberStrings = strings.Split(inputArr[0], ",")
	var selectedNumbersTemp [1000]int64
	for i, eachSelectedNumber := range selectedNumberStrings {
		var num, err = strconv.ParseInt(eachSelectedNumber, 10, 16)
		check(err)
		selectedNumbersTemp[i] = num
	}
	var earliestWinner result
	var latestWinner result
	var selectedNumbers = selectedNumbersTemp[0:len(selectedNumberStrings)]
	for i := 1; i < len(inputArr); i = i + 6 {
		var horizontalRows [5][5]int64
		var verticalRows [5][5]int64
		var eachBoard = inputArr[i+1 : i+6]
		for j, eachRow := range eachBoard {
			var eachNumbers = strings.Fields(eachRow)
			var eachNumbersArr [5]int64
			for k, eachNumber := range eachNumbers {
				var num, err = strconv.ParseInt(eachNumber, 10, 16)
				check(err)
				eachNumbersArr[k] = num
				verticalRows[k][j] = num
			}
			horizontalRows[j] = eachNumbersArr
		}
		var allRows [10][5]int64
		for i := 0; i < len(horizontalRows); i++ {
			allRows[i] = horizontalRows[i]
			allRows[i+5] = verticalRows[i]
		}

		var position, number, sum = findWinningRows(allRows, selectedNumbers)
		if position < earliestWinner.position || i == 1 {
			earliestWinner = result{position: position, number: number, sum: sum}
		}
		if position > latestWinner.position || i == 1 {
			latestWinner = result{position: position, number: number, sum: sum}
		}
	}
	println(earliestWinner.sum * earliestWinner.number)
	println(latestWinner.sum * latestWinner.number)
}

func findWinningRows(allRows [10][5]int64, selectedNumbers []int64) (int, int, int) {
	for numIdx, num := range selectedNumbers {
		for allRowIdx, row := range allRows {
			var sumOfRow int
			for numIdx, numInRow := range row {
				if numInRow == num {
					allRows[allRowIdx][numIdx] = -1
					sumOfRow = sumOfRow - 1
				} else {
					sumOfRow = sumOfRow + int(numInRow)
				}
			}
			if sumOfRow == -5 {
				return numIdx, int(num), sumRows(allRows[0:5])
			}
		}
	}
	return -1, -1, -1
}

func sumRows(allRows [][5]int64) int {
	var total int
	for i := range allRows {
		for j := range allRows[i] {
			if allRows[i][j] != -1 {
				total = total + int(allRows[i][j])
			}
		}
	}
	return total
}
