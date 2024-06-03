export function splitThousands(val: number, noFloor = true, abs = false) {
    if (isNaN(val)) {
        return val;
    }

    if (noFloor) {
        val = Math.floor(Number(val));
    }

    if (abs) {
        val = Math.abs(Number(val));
    }

    return val
        .toString()
        .replace(/\.\D/g, '')
        .replace(/\B(?=(\d{3})+(?!\d))/g, ' ');
}
