import './quickSort';
describe('quickSort', () => {
    it('should sort items by property', () => {
        const items = [
            { id: 3 },
            { id: 1 },
            { id: 2 }
        ];
        
        const sorted = items.quickSort(i => i.id);
        expect(sorted[0]).toEqual(items[1]);
        expect(sorted[1]).toEqual(items[2]);
        expect(sorted[2]).toEqual(items[0]);
    });
    
    it('should not sort items that are already ordered', () => {
        const items = [
            { id: 1 },
            { id: 2 },
            { id: 3 }
        ];
        
        const sorted = items.quickSort(i => i.id);
        expect(items[0]).toEqual(sorted[0]);
        expect(items[1]).toEqual(sorted[1]);
        expect(items[2]).toEqual(sorted[2]);
    });
    
    it('should use infinity for undefined item properties', () =>{
        const items = [
            { id: 5 },
            { id: undefined },
            { id: 3 }
        ];
        
        const sorted = items.quickSort(i => i.id);
        expect(sorted[0]).toEqual(items[2]);
        expect(sorted[1]).toEqual(items[0]);
        expect(sorted[2]).toEqual(items[1]);
    })
})