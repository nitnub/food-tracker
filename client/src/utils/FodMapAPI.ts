interface FodMapItem {
  name: string;
  fodmapId: number | null;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
}

export default class FodMapAPI {
  // private API_PORT;
  constructor() {
    // this.API_PORT = process.env.API_PORT
  }

  // addFood = async (foodItem: FodMapItem) => {
  //   const foodItemS = {
  //     name: foodItem.name,
  //     fodmapId: foodItem.fodmapId,
  //     vegetarian: foodItem.vegetarian,
  //     vegan: foodItem.vegan,
  //     glutenFree: foodItem.glutenFree,
  //   };

  //   const requestOptions = {
  //     method: 'POST',
  //     headers: { 'Content-Type': 'application/json' },
  //     body: JSON.stringify(foodItemS),
  //   };

  //   const resp = await fetch(
  //     `http://localhost:3200/api/v1/food`,
  //     requestOptions
  //   );

  //   return await resp.json();

  // };
  getAll = async () => {
    const res = await fetch('http://localhost:3200/api/v1/fodmap');
    const json = await res.json();
    if (json.status !== 'success') {
      throw new Error('Whoops - unable to fetch reactions!');
    }
    return json;
  };
}


