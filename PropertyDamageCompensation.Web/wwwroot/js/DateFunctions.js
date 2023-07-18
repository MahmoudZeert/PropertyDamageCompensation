function AddSubstractMonth(date, MonthNb) {
    return new Date(date.setMonth(date.getMonth() + MonthNb));
}