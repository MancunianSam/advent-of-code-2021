#include <stdio.h>
#include <stdlib.h>

void part_one();

void part_two();

int input[300] = {1, 2, 1, 3, 2, 1, 1, 5, 1, 4, 1, 2, 1, 4, 3, 3, 5, 1, 1, 3, 5, 3, 4, 5, 5, 4, 3, 1, 1, 4, 3, 1, 5,
                  2, 5, 2, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 4, 4, 4, 1, 4, 4, 1, 4, 2, 1, 1, 1, 1, 3, 5, 4, 3, 3, 5, 4,
                  1, 3, 1, 1, 2, 1, 1, 1, 4, 1, 2, 5, 2, 3, 1, 1, 1, 2, 1, 5, 1, 1, 1, 4, 4, 4, 1, 5, 1, 2, 3, 2, 2,
                  2, 1, 1, 4, 3, 1, 4, 4, 2, 1, 1, 5, 1, 1, 1, 3, 1, 2, 1, 1, 1, 1, 4, 5, 5, 2, 3, 4, 2, 1, 1, 1, 2,
                  1, 1, 5, 5, 3, 5, 4, 3, 1, 3, 1, 1, 5, 1, 1, 4, 2, 1, 3, 1, 1, 4, 3, 1, 5, 1, 1, 3, 4, 2, 2, 1, 1,
                  2, 1, 1, 2, 1, 3, 2, 3, 1, 4, 5, 1, 1, 4, 3, 3, 1, 1, 2, 2, 1, 5, 2, 1, 3, 4, 5, 4, 5, 5, 4, 3, 1,
                  5, 1, 1, 1, 4, 4, 3, 2, 5, 2, 1, 4, 3, 5, 1, 3, 5, 1, 3, 3, 1, 1, 1, 2, 5, 3, 1, 1, 3, 1, 1, 1, 2,
                  1, 5, 1, 5, 1, 3, 1, 1, 5, 4, 3, 3, 2, 2, 1, 1, 3, 4, 1, 1, 1, 1, 4, 1, 3, 1, 5, 1, 1, 3, 1, 1, 1,
                  1, 2, 2, 4, 4, 4, 1, 2, 5, 5, 2, 2, 4, 1, 1, 4, 2, 1, 1, 5, 1, 5, 3, 5, 4, 5, 3, 1, 1, 1, 2, 3, 1,
                  2, 1, 1};

int main() {
  part_one();
  part_two();
  exit(0);
}

void part_two() {
  int days = 256;
  int i, k;
  unsigned long long totals[9] = {0, 0, 0, 0, 0, 0, 0, 0, 0};
  for (i = 0; i < 300; i++) {
    totals[input[i]] = totals[input[i]] + 1;
  }
  for (i = 1; i <= days; ++i) {
    unsigned long long new[9] = {totals[1], totals[2], totals[3], totals[4], totals[5], totals[6], totals[7] + totals[0],
                                       totals[8], totals[0]};
    for (k = 0; k < 9; ++k) {
      totals[k] = new[k];
    }
  }
  unsigned long long sum = 0;
  for (k = 0; k < 9; ++k) {
    sum = sum + totals[k];
  }
  printf("Part two %llu\n", sum);
}

void part_one() {
  int days = 80;
  int i, j, idx, each, current, count = 300;
  int *ptr;
  ptr = (int *) calloc(1000000000, sizeof(int));
  if (ptr == NULL) {
    exit(0);
  } else {
    current = count;

    for (each = 0; each < count; ++each) {
      ptr[each] = input[each];
    }
    for (j = 1; j <= days; ++j) {
      idx = current;
      for (i = 0; i < idx; ++i) {
        int fish = ptr[i];
        if (fish == 0) {
          ptr[current] = 8;
          ptr[i] = 6;
          ++current;
          ++count;
        } else {
          --fish;
          ptr[i] = fish;
        }
      }
    }

    free(ptr);
  }
  printf("\nPart one %d\n", count);
}
