export function getCookie(key: string) {
    const b = document.cookie.match("(^|;)\\s*" + key + "\\s*=\\s*([^;]+)");
    return b ? b.pop() : "";
}

// total price
export function currencyFormat(amount: number) {
    return '₺' + (amount / 100).toFixed(2);
}