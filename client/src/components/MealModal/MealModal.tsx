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
import FoodPicker from '../FoodPicker';
import { useContext, useEffect, useState } from 'react';
// import Chip from '@mui/material/Chip';
// import styles from './ChipToggle.module.css';
// import { FoodDbResponse } from '../../../types/food.types';
import AppContext from '../../context/AppContext';
import { ActiveFood } from '../../context/AppContext';
import ChipToggle from '../Chip/ChipToggle';
// import AppContext, { defaultContext } from './context/AppContext';
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

export default function MealModal() {
  const [open, setOpen] = React.useState(false);
  const [value, setValue] = React.useState<Dayjs | null>(dayjs(dayjs())); // default to Now
  const [meal, setMeal] = React.useState<ActiveFood[]>([]);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const { appContext, setAppContext } = useContext(AppContext);
  // console.log(app);
  console.log(appContext.activeFood);

  useEffect(() => {
    const activeFood = appContext.activeFood;
    const mealExists = meal.some((m) => m.id === activeFood.id);
    console.log('meal exitst:', mealExists);
    if (mealExists) {
      setMeal(() => meal.filter((m) => m.id !== activeFood.id));
    } else {
      setMeal(() => [...meal, appContext.activeFood]);
    }
  }, [appContext.activeFood]);
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

  const mealHandler = (e: any) => {
    console.log('mh:', e);
  };

  const [active, setActive] = useState(-1);
  const toggleState = { active, setActive };

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
            {meal.map((m, index) => (
              <ChipToggle
                onClick={mealHandler}
                toggleState={toggleState}
                key={index}
                toggleId={index}
                foodItem={m}
              />
            ))}
            <Divider />
            <FoodPicker />
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
