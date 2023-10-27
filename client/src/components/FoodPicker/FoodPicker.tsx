import { useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import ChipToggleContainer from '../Chip/ChipToggleContainer';

export default function FoodPicker() {
  const [foodArr, setFoodArr] = useState([]);

  useEffect(() => {
    const getFood = async () => {
      const res = await fetch(process.env.REACT_APP_API_GET_ALL_FOODS || '');
      const json = await res.json();

      setFoodArr(() => json.data);
    };
    getFood();
  }, []);

  const { appContext } = useContext(AppContext);

  if (appContext.user.id === 0) {
    return <>Please select a valid user!</>;
  }
  return <ChipToggleContainer foodArray={foodArr} />;
}
