def get_range(arr)
  if arr[0] <= arr[1]
    arr[0]..arr[1]
  else
    arr[0].downto(arr[1])
  end
end

def produce_grid(lines, part_two = false)
  grid = []
  (0..1000).each { |idx| grid[idx] = Array.new(1000, 0)  }
  lines.each { |line|
    pair = line.split(" -> ")
    pair_one = pair[0].split(",")
    pair_two = pair[1].split(",")
    x_values = [Integer(pair_one[0]), Integer(pair_two[0])]
    y_values = [Integer(pair_one[1]), Integer(pair_two[1])]
    x_range = get_range x_values
    y_range = get_range y_values
    if x_values[0] == x_values[1] || y_values[0] == y_values[1]
      x_range.each { |x|
        y_range.each { |y|
          grid[y][x] = grid[y][x] + 1
        }
      }
    elsif part_two
      each_value = x_range.zip(y_range)
      each_value.each { |value|
        grid[value[1]][value[0]] = grid[value[1]][value[0]] + 1
      }
    end
  }
  grid.inject(0) {|sum, a| sum + a.filter{|x_val| x_val > 1}.count}
end

module Ruby
  file = File.open("input.txt")
  lines = file.readlines(chomp: true)

  puts produce_grid lines
  puts produce_grid lines, true
end
