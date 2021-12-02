defmodule AOC do
  def heading_part_one(rows) do
    {horizontal, depth} =
      Enum.reduce(
        rows,
        {0, 0},
        fn row, {ha, da} ->
          {h, d} = instructions(row, {0, 0})
          {h + ha, d + da}
        end
      )

    horizontal * depth
  end

  def heading_part_two(rows) do
    {horizontal, depth, _} =
      Enum.reduce(
        rows,
        {0, 0, 0},
        fn row, {ha, da, aa} ->
          {h, d, a} = instructions(row, {0, 0, aa})
          {h + ha, d + da, a}
        end
      )

    horizontal * depth
  end

  def instructions(row, positions) do
    [instruction, amount] = String.split(row, " ")
    {amount_int, _} = Integer.parse(amount)
    move(instruction, amount_int, positions)
  end

  def move("forward", amount, {horizontal, depth}) do
    {horizontal + amount, depth}
  end

  def move("up", amount, {horizontal, depth}) do
    {horizontal, depth - amount}
  end

  def move("down", amount, {horizontal, depth}) do
    {horizontal, depth + amount}
  end

  def move("forward", amount, {horizontal, depth, aim}) do
    {horizontal + amount, depth + aim * amount, aim}
  end

  def move("up", amount, {horizontal, depth, aim}) do
    {horizontal, depth, aim - amount}
  end

  def move("down", amount, {horizontal, depth, aim}) do
    {horizontal, depth, aim + amount}
  end
end

{:ok, rows} = File.read("input.txt")
IO.puts(AOC.heading_part_one(String.split(rows, "\n")))
IO.puts(AOC.heading_part_two(String.split(rows, "\n")))
