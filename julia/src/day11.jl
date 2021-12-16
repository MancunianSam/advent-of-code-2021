using DelimitedFiles

file = readdlm("src/input.txt", Int)

function values(initial:: CartesianIndex)
  x = initial[1]
  y = initial[2]
  arr = [CartesianIndex(x + 1, y), CartesianIndex(x - 1, y), CartesianIndex(x, y + 1), CartesianIndex(x, y - 1), CartesianIndex(x + 1, y - 1), CartesianIndex(x - 1 , y - 1), CartesianIndex(x + 1, y + 1), CartesianIndex(x - 1 , y + 1)]
  filter(x -> x[1] > 0 && x[1] <= 10 && x[2] > 0 && x[2] <= 10, arr)
end

function process(input, range)
  count = 0
  for i in 1:range
    for j in eachindex(input)
      input[j] = input[j] == 9 ? 0 : input[j] + 1
    end
    function triggered(to_check)
      existing_zero = findall(x -> x == 0, input)
      new_points = collect(Iterators.flatten(map(x -> values(x), to_check)))
      for point in new_points
        if(input[point] != 0)
          input[point] = input[point] == 9 ? 0 : input[point] + 1
        end
      end
      now_zero = findall(x -> x == 0, input)
      current_zero = now_zero[now_zero .∉ Ref(existing_zero)]
      l = length(current_zero)
      if(l > 0)
        count = count + l
        triggered(current_zero)
      else
        return current_zero
      end
    end

    now_zero = findall(x -> x == 0, input)
    if(!isempty(now_zero))
      count = count + length(now_zero)
      triggered(now_zero)
    end
    now_zero = findall(x -> x == 0, input)
    if(length(now_zero) == 100)
        print("all zeros")
        println(i)
    end
  end
  return count
end

println(process(file, 100))
println(process(file, 520))


