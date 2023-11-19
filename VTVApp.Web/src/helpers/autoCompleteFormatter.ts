export interface AutocompleteOption {
  label: string;
  id: number | string;
  uniqueKey: string;
  type: "province" | "city";
}

export const autocompleteFormatter = <
  T extends { id: number | string; name: string }
>(
  data: T[],
  type: "province" | "city"
): AutocompleteOption[] => {
  return data.map((element, index) => ({
    label: element.name,
    id: element.id,
    uniqueKey: `${element.id}-${element.name}-${index}`,
    type: type,
  }));
};
