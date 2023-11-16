interface FoodItem {
  name: string;
  fodmapId: number | null;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
}

class FoodAPI {
  // private API_PORT;
  constructor() {
    // this.API_PORT = process.env.API_PORT
  }

  addFood = async (foodItem: FoodItem) => {
    const reqBody = { data: [foodItem] };
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(reqBody),
    };

    const resp = await fetch(
      `http://localhost:3200/api/v1/food`,
      requestOptions
    );

    return await resp.json();
  };
}
const instance = new FoodAPI();

export default instance;
