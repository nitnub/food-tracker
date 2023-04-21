import TextField from '@mui/material/TextField';
import { ChangeEvent, useState } from 'react';
// import ChangeEvent from 'react'
import FormLabel from '@mui/material/FormLabel';
import FormControl from '@mui/material/FormControl';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormHelperText from '@mui/material/FormHelperText';
import Checkbox from '@mui/material/Checkbox';
import { Button } from '@mui/material';
import FoodAPI from '../../utils/FoodAPI';

export default function UpdateContainer() {
  const defaultFoodForm = {
    name: '',
    fodmapId: null,
    vegetarian: false,
    vegan: false,
    glutenFree: false,
  };

  const defaultErrorState = {
    name: false,
    fodmapId: false,
    vegetarian: false,
    vegan: false,
    glutenFree: false,
  };

  const updateStatusDefault = {
    success: false,
    fail: '',
    loading: false,
  };

  // type ClickOption = 'food' | 'fodmapId' | 'vegetarian' | 'vegan' | 'glutenFree'
  type CheckOption = 'vegetarian' | 'vegan' | 'glutenFree';
  const [foodForm, setFoodForm] = useState(defaultFoodForm);
  const [errorState, setErrorState] = useState(defaultErrorState);
  const [isUpdating, setIsUpdating] = useState(false);
  const [updateStatus, setUpdateStatus] = useState(updateStatusDefault);
  const fAPI = FoodAPI;

  const checkHandler = (e: ChangeEvent<HTMLInputElement>) => {
    const clicked: CheckOption = e.target.name as CheckOption;

    if (clicked === 'vegan' && foodForm.vegan === false) {
      setFoodForm({ ...foodForm, vegan: true, vegetarian: true });
    } else if (clicked === 'vegetarian' && foodForm.vegetarian === true) {
      setFoodForm({ ...foodForm, vegetarian: false, vegan: false });
    } else if (foodForm[clicked] === false) {
      setFoodForm({ ...foodForm, [clicked]: true });
    } else if (foodForm[clicked] === true) {
      setFoodForm({ ...foodForm, [clicked]: false });
    }
  };

  const submitHandler = async () => {
    // setUpdateStatus({...updateStatus, loading: true})
    setUpdateStatus((updateStatus) => ({ ...updateStatus, loading: true }));
    console.log(foodForm);

    const apiResponse = await fAPI.addFood(foodForm);
    console.log('Status:');
    console.log(apiResponse);
    if (apiResponse.status !== 'success') {
      // we had an error
      setUpdateStatus((updateStatus) => ({
        ...updateStatus,
        fail: apiResponse.message,
      }));
      setTimeout(() => {
        setUpdateStatus((updateStatus) => ({ ...updateStatus, fail: '' }));
        // setUpdateStatus({...updateStatus, success: false})
      }, 3000);
    } else {
      // success message..
      setFoodForm(defaultFoodForm);
      console.log(updateStatus);
      setUpdateStatus((updateStatus) => ({ ...updateStatus, success: true }));
      setTimeout(() => {
        setUpdateStatus((updateStatus) => ({
          ...updateStatus,
          success: false,
        }));
        // setUpdateStatus({...updateStatus, success: false})
      }, 3000);
    }
    setUpdateStatus((updateStatus) => ({ ...updateStatus, loading: false }));
    // setUpdateStatus({...updateStatus, loading: false})
  };

  return (
    <>
      {' '}
      {/* <TextField
        // className="form-control"
        // id={id}
        // name={id}
        // name="Name"
        // label={label}
        label="Name"
        value={foodForm.food} //
        onChange={(e) => setFoodForm({ ...foodForm, food: e.target.value })} //
        // error={formik.touched[name] && Boolean(formik.errors[name])} //
        error={errorState.food} //
        // helperText={formik.touched[id] && formik.errors[id]} //
        helperText="The name that will appear for this food item" //
        // type={type}
        variant="standard"
        // min="0" // Hold for now in case of need for a dynamic num form field
      /> */}
      <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
        <FormLabel component="legend">Add Food</FormLabel>
        <FormGroup>
          <TextField
            // className="form-control"
            // id={id}
            // name={id}
            // name="Name"
            // label={label}
            label="Name"
            value={foodForm.name} //
            onChange={(e) => setFoodForm({ ...foodForm, name: e.target.value })} //
            // error={errorState.food} //
            helperText="The name that will appear for this food item" //
            // type={type}
            variant="standard"
          />

          <FormControlLabel
            control={
              <Checkbox
                checked={foodForm.vegetarian}
                onChange={checkHandler}
                name="vegetarian"
              />
            }
            label="Vegetarian"
          />
          <FormControlLabel
            control={
              <Checkbox
                checked={foodForm.vegan}
                onChange={checkHandler}
                name="vegan"
              />
            }
            label="Vegan"
          />
          <FormControlLabel
            control={
              <Checkbox
                checked={foodForm.glutenFree}
                onChange={checkHandler}
                name="glutenFree"
              />
            }
            label="Gluten Free"
          />
        </FormGroup>
        <Button
          disabled={updateStatus.loading}
          onClick={submitHandler}
          variant="contained"
          size="small"
        >
          Submit
        </Button>
        {(updateStatus.loading && (
          <FormHelperText>Sending data...</FormHelperText>
        )) ||
          (updateStatus.success && (
            <FormHelperText>Food added!</FormHelperText>
          ))}
        {updateStatus.fail && (
          <FormHelperText>{updateStatus.fail}</FormHelperText>
        )}
      </FormControl>
    </>
  );
  // function toggleKey(key: CheckOption, obj: foodForm) {
  //   foodForm[key]

  // };
}
