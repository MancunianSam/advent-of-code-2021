use std::io::{BufRead, BufReader, Lines};
use std::fs::{File};
use std::ops::Mul;
use std::path::Path;

fn main() {
  part_one();
  part_two()
}

fn get_lines() -> Lines<BufReader<File>> {
  let path = Path::new("input.txt");
  let file = File::open(&path).unwrap();
  BufReader::new(file).lines()
}

fn get_bit_at(input: u16, n: i32) -> u32 {
  if input & (1 << n) != 0 {1} else {0}
}

fn get_values() -> (u16, u16) {
  let lines = get_lines();
  let init = vec![0; 12];
  let mapped = lines.fold(init, |acc: Vec<u32>, a| {
    let str = a.unwrap();
    let len = str.chars().count() as i32;
    let int_val = u16::from_str_radix(&str, 2).unwrap();
    let each = (0 as i32..len).map(|i| {
      let next_val: u32 = get_bit_at(int_val, i);
      let acc_new = acc.get(i as usize).unwrap() + next_val;
      acc_new
    }).collect();
    return each
  });
  let gamma = produce_string(&mapped,"1", "0");
  let epsilon = produce_string(&mapped,"0", "1");
  (gamma, epsilon)
}

fn part_one() {
  let (gamma, epsilon) = get_values();
  println!("{}", (gamma as u64).mul(epsilon as u64));
}

fn part_two() {
  let lines: Vec<String> = get_lines().map(|f| f.unwrap()).into_iter().collect();
  let oxygen = filter_each(11, lines.to_owned(), 1, 0);
  let co2 = filter_each(11, lines.to_owned(), 0, 1);
  let res = i64::from_str_radix(&oxygen, 2).unwrap().mul(i64::from_str_radix(&co2, 2).unwrap());
  println!("{}", res)
}

fn filter_each(i: i32, lines: Vec<String>, high_val: i32, low_val: i32) -> String {
  if lines.len() == 1 || i < 0 {
    lines.join("")
  } else {
    let new_lines = filter(i, lines, high_val, low_val);
    filter_each(i - 1, new_lines, high_val, low_val)
  }
}

fn get_most_common_bit(lines: Vec<String>, i: i32, high_val: i32, low_val: i32) -> i32 {
  let v = lines.to_owned().into_iter().fold((0, 0), |acc: (i32, i32), s| {
    let num_from_str = u16::from_str_radix(&s, 2).unwrap();
    let bit_at = get_bit_at(num_from_str, i as i32);
    let (one, zero) = acc;
    let new_acc = if bit_at == 1 {(one + 1, zero)} else {(one, zero + 1)};
    new_acc
  });
  return if v.0 >= v.1 { high_val } else { low_val };
}

fn filter(i: i32, lines: Vec<String>, high_val: i32, low_val: i32) -> Vec<String> {
  let most_common_bit = get_most_common_bit(lines.to_owned(), i, high_val, low_val);
  lines.to_owned().into_iter().filter(|s| {
    let int_val = u16::from_str_radix(&s, 2).unwrap();
    let input_bit = get_bit_at(int_val, i) as i32;
    most_common_bit == input_bit
  }).collect()

}

fn produce_string(mapped: &Vec<u32>, larger: &str, smaller: &str) -> u16 {
  let lines_count = 1000;
  let c: Vec<&str> = (0 as u32..12).map(|f| {
  let sum = mapped.get(f as usize).unwrap();
    if sum > &(lines_count as u32 - sum) {larger} else {smaller}
  }).collect();
  let binary_str = c.join("").chars().rev().collect::<String>();
  return u16::from_str_radix(&binary_str, 2).unwrap()
}
