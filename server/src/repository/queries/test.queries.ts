export const getAllTests = () => {
  return `
    SELECT * FROM Test;
  `;
};

export const getAllMatchingTests = (name: string) => {
  return `
    SELECT * FROM Test WHERE LOWER(name) LIKE '%${name}%';
  `;
};

interface TestDBObject {
  id: number
  porkInt: number
  porkBool: boolean 
  porkText: string
}

export const addTest = (TestItem: TestDBObject) => {
  const { id, porkInt, porkBool, porkText } = TestItem;

  return `
    INSERT INTO Test(name, fodmap_id, vegetarian, vegan, gluten_free) 
    VALUES (
      '${name}'
      , ${fodmapId}
      , ${vegetarian}
      , ${vegan}
      , ${glutenFree}      
    );
  `;
};

export const deleteTest = (TestId: number) => {
  return `
      DELETE FROM test_table
      WHERE id = ${TestId};
    `;
};
