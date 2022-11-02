import omitIsNil from "./omit"
import axios from "axios"
import Cookies from "js-cookie"

const makeRequest = async ({ method, url, data, headerOpt, params }) => {
  let options = {
    method,
    url,
    data,
    headers: { "x-access-token": Cookies.get("x-access-token"), ...headerOpt },
    params: params,
  }

  options = omitIsNil(options)

  const result = await axios(options)
  return result.data
}

export const parseRequestParams = (params) => {
  let parseParams = new URLSearchParams()
  for (const [key, value] of Object.entries(params)) {
    if (Array.isArray(value)) {
      value.forEach((item) => {
        parseParams.append(key, item)
      })
    } else {
      parseParams.append(key, value)
    }
  }
  return parseParams
}

export default makeRequest
