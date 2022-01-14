import { decode } from "./utils/jwt";
import {appStore} from "./redux/store";

export const loadState = () => {
    try {
      const token = localStorage.getItem('token');
      if (token === null) {
        return undefined;
      }
      return decode(token);
    } catch (err) {
      return undefined;
    }
};

export const saveState = (state) => {
	try {
	const serialState = appStore.getState().auth.token;
	localStorage.setItem('token', serialState);
	} catch(err) {
		console.log(err);
	}
};
