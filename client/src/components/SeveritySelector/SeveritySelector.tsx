import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormLabel from '@mui/material/FormLabel';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import { MouseEvent, useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import { Severity } from '../../types/dbTypes';
import styles from './SeveritySelector.module.css';
import PendingIcon from '@mui/icons-material/Pending';


// export default function Radio(severities: Severity[]) {
export default function SeveritySelector({
  // reactionTypeId,
  reactionType,
  severities,
  categoryId,
  // value
}: {
  // reactionTypeId: number;
  reactionType: any;
  categoryId: number;
  severities: Severity[];
  // value: number | null;
}) {
  const { appContext, setAppContext } = useContext(AppContext);
  const [updatingSeverity, setUpdatingSeverity] = useState(false);
  const [value, setValue] = useState(0)


  const handler = async (e: any) => {
    console.log(e.target.value)
    console.log(value)
    setValue( e.target.value)
    setUpdatingSeverity(() => true)
    const updatedReaction = {
      severityId: Number(e.target.value),
      // reactionTypeId,
      reactionTypeId: reactionType.id,
      elementId: appContext.activeFood.id,
    };
    // console.log(updatedReaction);
    // console.log(JSON.parse(e))
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(updatedReaction)
  };
    // console.log('AC:', appContext)
    // console.log('AC:', updatedReaction)
    const resp = await fetch(`http://localhost:3200/api/v1/reaction/${appContext.user.userId}`, requestOptions);
    const json = await resp.json();
    // console.log(json)
    setUpdatingSeverity(() => false)
  };

 


  // const {appContext, setAppContext} = useContext(AppContext);
  // console.log('aaaaaaaaaaaafffff', appContext.activeFood.reactions)
  const existingReactions = appContext.activeFood.reactions || []
  
  // let value = null;

  useEffect(() => {
    // console.log('aa')
    // console.log(reaction)
    setValue(0)
    for(let reaction of existingReactions) {
      // console.log(reaction, reactionType)
      if (reaction.reactionType === reactionType.id) {
        // value = reactionType.id
        // console.log(reaction)
        // console.log('bb');
        // console.log(reaction.severity)
        setValue(reaction.severity)
      }
    }
    // console.log('render now')

  },[appContext.activeFood.id])

  // console.log(value)

  return (
    <>
   
   
      <FormControl>
        {/* <FormLabel id="demo-row-radio-buttons-group-label">Gender</FormLabel> */}

        <RadioGroup
          onChange={handler}
          // value={value}
          value={value}
          row
          aria-labelledby="demo-row-radio-buttons-group-label"
          name="row-radio-buttons-group"
        >
          {severities.map((severity: Severity, index: number) => {
            return (
              <FormControlLabel
              key={index}
                className={styles.radioLabel}
                value={
                  severity.id}
                control={<Radio size="small" />}
                label={severity.name}
              />
            );
          })}

          {/* <FormControlLabel value="female" control={<Radio />} label="Female" />
          <FormControlLabel value="male" control={<Radio />} label="Male" />
          <FormControlLabel value="other" control={<Radio />} label="Other" /> */}
        </RadioGroup>
      </FormControl>
      {updatingSeverity && <PendingIcon />}
    </>
  );
  // return (
  //   <>
  //     {severities.map((severity: Severity, index: number) => {
  //       return (
  //         <>
  //           <div key={`i${index}`}>
  //             <input
  //               key={`i${index}`}
  //               type="radio"
  //               id={severity.id.toString()}
  //               name="severity"
  //               value={severity.name}
  //             />
  //             <label key={`${index}`} htmlFor={severity.name}>
  //               {severity.name}{' '}
  //             </label>
  //           </div>

  //           <FormControl>
  //             <FormLabel id="demo-row-radio-buttons-group-label">
  //               Gender
  //             </FormLabel>

  //             <RadioGroup
  //               row
  //               aria-labelledby="demo-row-radio-buttons-group-label"
  //               name="row-radio-buttons-group"
  //             >
  //               <FormControlLabel
  //                 value="female"
  //                 control={<Radio />}
  //                 label="Female"
  //               />
  //               <FormControlLabel
  //                 value="male"
  //                 control={<Radio />}
  //                 label="Male"
  //               />
  //               <FormControlLabel
  //                 value="other"
  //                 control={<Radio />}
  //                 label="Other"
  //               />

  //             </RadioGroup>

  //           </FormControl>
  //         </>
  //       );
  //     })}
  //   </>
  // );
}
