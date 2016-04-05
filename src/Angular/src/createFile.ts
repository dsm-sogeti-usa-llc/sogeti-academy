export function createFile(name?: string, type?: string, size?: number): File {
        return {
            type: type || '',
            size: size || 0,
            lastModifiedDate: 0,
            msClose: null,
            msDetachStream: null,
            name: name || '',
            slice: null
        }
    }