import FodSearch from '../FodSearch';

export default function FodMapPicker({
  foodForm,
  setFoodForm,
}: {
  foodForm: object;
  setFoodForm: Function;
}) {
  return (
    <>
      <FodSearch foodForm={foodForm}  setFoodForm={setFoodForm} />
    </>
  );
}
