import { Button } from '@mui/material';
import Chip from '@mui/material/Chip';
import { useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import ChipToggle from '../Chip/ChipToggle';
import ChipToggleContainer from '../Chip/ChipToggleContainer';

interface FoodChip {
  id: number;
  name: string;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
  fodmapId: number;
}

export default function FoodPicker(
  // {foodState}: {foodState: object}
  ) {
    const [foodArr, setFoodArr] = useState([]);

    useEffect(() => {
      const getFood = async () => {
        const res = await fetch(process.env.REACT_APP_API_GET_ALL_FOODS || '');
        const json = await res.json();
        // console.log('Food Array:', json.data);
        setFoodArr(() => json.data);
      };
      getFood();
    }, []);
    
    


  const {appContext, setAppContext} = useContext(AppContext);

  if (appContext.user.id === 0) {
    return <>Please select a valid user!</>;
  }
  return (
      <ChipToggleContainer 
      // foodState={foodState} 
      foodArray={foodArr} />
    // <div>
    //   {foodArr.map((el: FoodChip, index) => (
        
        
        
    //     // <Button>
    //     //   <Chip
    //     //     variant="outlined"
    //     //     color="secondary"
    //     //     size="small"
    //     //     label={`${el.name}`}
    //     //   />
    //     // </Button>
    //   ))}
    // </div>
  );
}
