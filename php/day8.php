<?php
$file = file_get_contents("input.txt");
$arr = explode("\n", $file);

function lengths($var): bool
{
    return in_array(strlen($var), [2, 3, 4, 7]);
}

function sort_key($str): string
{
    $str_arr = str_split($str);
    sort($str_arr);
    return join($str_arr);
}


$count_part_one = 0;
$sum_part_two = 0;
foreach ($arr as $value) {
    $line = explode(" | ", $value);
    $signals = explode(" ", $line[0]);
    $digits = explode(" ", $line[1]);
    $filtered = array_filter($digits, "lengths");
    $seven = str_split(array_values(array_filter($signals, function ($elem) {
        return strlen($elem) == 3;
    }))[0]);
    $six = array_values(array_filter($signals, function ($elem) use ($seven) {
        return strlen($elem) == 6 && count(array_diff($seven, str_split($elem))) == 1;
    }))[0];
    $four = str_split(array_values(array_filter($signals, function ($elem) {
        return strlen($elem) == 4;
    }))[0]);
    $zero = array_values(array_filter($signals, function ($elem) use ($four, $six) {
        return strlen($elem) == 6 && $elem != $six && count(array_diff($four, str_split($elem))) == 1;
    }))[0];
    $one = str_split(array_values(array_filter($signals, function ($elem) {
        return strlen($elem) == 2;
    }))[0]);
    $eight = array_filter($signals, function ($elem) {
        return strlen($elem) == 7;
    });
    $nine = array_values(array_filter($signals, function ($elem) use ($zero, $six) {
        return strlen($elem) == 6 && $elem != $six && $elem != $zero;
    }))[0];
    $three = array_values(array_filter($signals, function ($elem) use ($seven) {
        return strlen($elem) == 5 && !array_diff($seven, str_split($elem));
    }))[0];
    $two = array_values(array_filter($signals, function ($elem) use ($four, $seven) {
        return strlen($elem) == 5 && count(array_diff($four, str_split($elem))) == 2;
    }))[0];
    $five = array_values(array_filter($signals, function ($elem) use ($two, $three) {
        return strlen($elem) == 5 && $elem != $three && $elem != $two;
    }))[0];
    $one = join($one);
    $four = join($four);
    $seven = join($seven);
    $eight = join($eight);
    $keys = array(sort_key($zero) => "0", sort_key($one) => "1", sort_key($two) => "2", sort_key($three) => "3", sort_key($four) => "4", sort_key($five) => "5", sort_key($six) => "6", sort_key($seven) => "7", sort_key($eight) => "8", sort_key($nine) => "9");
    $mapped = array_map(function ($digit) use ($keys) {
        $digit_arr = str_split($digit);
        sort($digit_arr);
        return $keys[join($digit_arr)];
    }, $digits);
    $value = intval(join($mapped));
    $sum_part_two = $sum_part_two + $value;
    $count_part_one = $count_part_one + count($filtered);
}
print $count_part_one . "\n";
print $sum_part_two . "\n";
?>