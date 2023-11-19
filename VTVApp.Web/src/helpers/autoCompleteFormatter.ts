export const autocompleteFormatter = (data) => {
    const pepe = data.map(element => {
        return {
            label: element.name,
            id: element.id
        }
    })
    return pepe;
}