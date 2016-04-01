export interface SortedArray<T> extends Array<T> {
}

declare global {
    interface Array<T> {
        quickSort(prop: (item: T) => number): SortedArray<T>
    }
}

Array.prototype.quickSort = function(prop: (item: any) => number) {
    if (this.length < 2)
        return this;
    
    const property = (i) => prop(i) || Infinity;
    const pivot = this[Math.round(this.length / 2)];
    return this.filter(i => property(i) < property(pivot))
        .quickSort(prop)
        .concat(this.filter(i => property(i) === property(pivot)))
        .concat(this.filter(i => property(i) > property(pivot)).quickSort(prop));
}