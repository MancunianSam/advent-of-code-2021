package example.day1

import scala.annotation.tailrec
import scala.io.Source

object DayOne {

  def input: List[Int] = {
    val source = Source.fromResource("day1")
    source.getLines.toList.map(_.toInt)
  }

  @tailrec
  def partTwo(input: List[Int], count: Int): Int = {
    if(input.length < 4) {
      count
    } else {
      val firstSum = input.head + input(1) + input(2)
      val secondSum = input(1) + input(2) + input(3)
      val newCount = if(firstSum < secondSum) 1 else 0
      partTwo(input.tail, newCount + count)
    }
  }

  @tailrec
  def partOne(input: List[Int], count: Int): Int = {
    if(input.length == 1) {
      count
    } else {
      val newCount = if(input.head < input.tail.head) 1 else 0
      partOne(input.tail, count + newCount)
    }
  }
}
