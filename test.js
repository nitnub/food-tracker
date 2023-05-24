let stops = false;
let count = 0;
do {
  console.log(count);
  count += 1;
  count > 10 && (stops = true);
} while (!stops);

console.log(stops);

let p = -4;
while (p < 5) {
  console.log(`p = ${p}`);
  p++;
}
