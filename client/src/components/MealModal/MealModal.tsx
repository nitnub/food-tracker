import * as React from 'react';
import Backdrop from '@mui/material/Backdrop';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import styles from './MealModal.module.css';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import Divider from '@mui/material/Divider';
import dayjs, { Dayjs } from 'dayjs';
import MealPicker from '../MealPicker';
import { useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import { ActiveFood } from '../../context/AppContext';
import MealChip from '../Chip/MealChip';

const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 500,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

export interface MealItem extends ActiveFood {
  partOfMeal?: boolean;
}

export default function MealModal() {
  const [open, setOpen] = React.useState(false);
  const [value, setValue] = React.useState<Dayjs | null>(dayjs(dayjs())); // default to Now
  const [foodArr, setFoodArr] = useState<ActiveFood[]>([]);
  const [active, setActive] = useState(-1);

  const toggleState = { active, setActive };
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const { appContext, setAppContext } = useContext(AppContext);


  useEffect(() => {
    const getFood = async () => {
      const res = await fetch(process.env.REACT_APP_API_GET_ALL_FOODS || '');
      const json = await res.json();

      setFoodArr(() =>
        json.data.map((el: MealItem) => ({ ...el, partOfMeal: false }))
      );
    };
    getFood();
  }, []);

  const onMonthChange = (e: any) => {
    //     Signature:
    // function(month: TDate) => void

    //     month The new month.
    console.log('month was changed in picker with argument', e);
  };
  const onYearChange = (e: any) => {
    // Signature:
    // function(year: TDate) => void

    //     year The new year.
    console.log('onYearChange was changed in picker with argument', e);
  };

  const mealHandler = (id: number) => {
    const newArr: MealItem[] = foodArr.map((el: MealItem) => {
      if (el.id === id) {
        el.partOfMeal = !el.partOfMeal;
      }
      return el;
    });
    setFoodArr(() => newArr);
  };



  return (
    <div>
      <Button onClick={handleOpen}>Open modal</Button>
      <Modal
        aria-labelledby="add-meal-modal"
        aria-describedby="add a meal"
        open={open}
        onClose={handleClose}
        closeAfterTransition
        slots={{ backdrop: Backdrop }}
        slotProps={{
          backdrop: {
            timeout: 500,
          },
        }}
      >
        <Fade in={open}>
          <Box sx={style}>
            <div className={styles.modalHeader}>
              <Typography id="add-meal-modal" variant="h6" component="h2">
                Add Meal
              </Typography>
              <DateTimePicker
                onMonthChange={onMonthChange}
                onYearChange={onYearChange}
                label="Estimated meal time"
                value={value}
                onChange={(newValue) => setValue(newValue)}
              />
            </div>
            <Divider />

            <Typography id="add a meal" sx={{ mt: 2 }}>
              // Time // search // chips // arrange by most recent update //
              modify existing // delete existing
            </Typography>
            <Divider />
            {foodArr
              .filter((el: MealItem) => el.partOfMeal)
              .map((m, index) => (
                <MealChip
                  clickHandler={mealHandler}
                  toggleState={toggleState}
                  key={index}
                  toggleId={index}
                  foodItem={m}
                />
              ))}
            <Divider />
            <MealPicker clickHandler={mealHandler} foodOptions={foodArr} />
            <Typography id="add a meal" sx={{ mt: 2 }}>
              chips // search // arrange by most recent update // add time //
              add reaction(s) // add severity // modify existing // delete
              existing
            </Typography>
          </Box>
        </Fade>
      </Modal>
    </div>
  );
}
