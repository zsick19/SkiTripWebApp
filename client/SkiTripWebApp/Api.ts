/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export enum UserRole {
  Standard = 0,
  Coordinator = 1,
  Admin = 2,
}

export interface CityLocation {
  /** @format guid */
  cityLocationId?: string;
  cityName?: string;
  isActive?: boolean;
  /** @format guid */
  cityDirectorId?: string | null;
  cityDirector?: User | null;
}

export interface User {
  /** @format guid */
  userId?: string;
  firstName?: string;
  lastName?: string;
  phoneNumber?: string;
  email?: string;
  homeAddress?: string;
  addressCity?: string;
  addressState?: string;
  addressZipCode?: string;
  role?: UserRole;
  attendingTrips?: SkiTrip[];
  coordinatedTrips?: SkiTrip[];
  personalGear?: SkiGear[];
}

export interface SkiTrip {
  /** @format guid */
  skiTripId?: string;
  tripTitle?: string;
  isPastTrip?: boolean;
  tripOriginCity?: string;
  tripOriginLocation?: string;
  tripDestinationLocation?: string;
  /** @format int32 */
  numberOfSeats?: number;
  /** @format date-time */
  tripOriginDepartureTime?: string;
  /** @format date-time */
  tripOriginArrivalTime?: string;
  /** @format date-time */
  tripDestinationDepartureTime?: string;
  /** @format date-time */
  tripDestinationArrivalTime?: string;
  isRoundTrip?: boolean;
  /** @format int32 */
  totalMiles?: number;
  /** @format double */
  totalCost?: number;
  /** @format guid */
  tripCoordinatorId?: string | null;
  tripCoordinator?: User | null;
  registeredUsers?: User[];
}

export interface SkiGear {
  /** @format guid */
  skiGearId?: string;
  gearName?: string;
  isStoredOnSite?: boolean;
  /** @format guid */
  gearOwnerId?: string;
  gearOwner?: User;
  /** @format guid */
  storageLocationId?: string | null;
  storageLocation?: StorageLocation | null;
}

export interface StorageLocation {
  /** @format guid */
  storageLocationId?: string;
  storageLocationName?: string;
  /** @format int32 */
  skiStorageCapacity?: number;
  storedGear?: SkiGear[];
}

export interface UserGearDTO {
  /** @format guid */
  userId?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  role?: UserRole;
  attendingTrips?: SkiTrip[];
  coordinatedTrips?: SkiTrip[];
  personalGear?: SkiGear[];
}

export type QueryParamsType = Record<string | number, any>;
export type ResponseFormat = keyof Omit<Body, "body" | "bodyUsed">;

export interface FullRequestParams extends Omit<RequestInit, "body"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat;
  /** request body */
  body?: unknown;
  /** base url */
  baseUrl?: string;
  /** request cancellation token */
  cancelToken?: CancelToken;
}

export type RequestParams = Omit<
  FullRequestParams,
  "body" | "method" | "query" | "path"
>;

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string;
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">;
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<RequestParams | void> | RequestParams | void;
  customFetch?: typeof fetch;
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown>
  extends Response {
  data: D;
  error: E;
}

type CancelToken = Symbol | string | number;

export enum ContentType {
  Json = "application/json",
  JsonApi = "application/vnd.api+json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = "http://localhost:5113";
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private abortControllers = new Map<CancelToken, AbortController>();
  private customFetch = (...fetchParams: Parameters<typeof fetch>) =>
    fetch(...fetchParams);

  private baseApiParams: RequestParams = {
    credentials: "same-origin",
    headers: {},
    redirect: "follow",
    referrerPolicy: "no-referrer",
  };

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig);
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key);
    return `${encodedKey}=${encodeURIComponent(typeof value === "number" ? value : `${value}`)}`;
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key]);
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key];
    return value.map((v: any) => this.encodeQueryParam(key, v)).join("&");
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {};
    const keys = Object.keys(query).filter(
      (key) => "undefined" !== typeof query[key],
    );
    return keys
      .map((key) =>
        Array.isArray(query[key])
          ? this.addArrayQueryParam(query, key)
          : this.addQueryParam(query, key),
      )
      .join("&");
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery);
    return queryString ? `?${queryString}` : "";
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.JsonApi]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.Text]: (input: any) =>
      input !== null && typeof input !== "string"
        ? JSON.stringify(input)
        : input,
    [ContentType.FormData]: (input: any) => {
      if (input instanceof FormData) {
        return input;
      }

      return Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key];
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
              ? JSON.stringify(property)
              : `${property}`,
        );
        return formData;
      }, new FormData());
    },
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  };

  protected mergeRequestParams(
    params1: RequestParams,
    params2?: RequestParams,
  ): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected createAbortSignal = (
    cancelToken: CancelToken,
  ): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken);
      if (abortController) {
        return abortController.signal;
      }
      return void 0;
    }

    const abortController = new AbortController();
    this.abortControllers.set(cancelToken, abortController);
    return abortController.signal;
  };

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken);

    if (abortController) {
      abortController.abort();
      this.abortControllers.delete(cancelToken);
    }
  };

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const queryString = query && this.toQueryString(query);
    const payloadFormatter = this.contentFormatters[type || ContentType.Json];
    const responseFormat = format || requestParams.format;

    return this.customFetch(
      `${baseUrl || this.baseUrl || ""}${path}${queryString ? `?${queryString}` : ""}`,
      {
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData
            ? { "Content-Type": type }
            : {}),
        },
        signal:
          (cancelToken
            ? this.createAbortSignal(cancelToken)
            : requestParams.signal) || null,
        body:
          typeof body === "undefined" || body === null
            ? null
            : payloadFormatter(body),
      },
    ).then(async (response) => {
      const r = response as HttpResponse<T, E>;
      r.data = null as unknown as T;
      r.error = null as unknown as E;

      const responseToParse = responseFormat ? response.clone() : response;
      const data = !responseFormat
        ? r
        : await responseToParse[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data;
              } else {
                r.error = data;
              }
              return r;
            })
            .catch((e) => {
              r.error = e;
              return r;
            });

      if (cancelToken) {
        this.abortControllers.delete(cancelToken);
      }

      if (!response.ok) throw data;
      return data;
    });
  };
}

/**
 * @title My Title
 * @version 1.0.0
 * @baseUrl http://localhost:5113
 */
export class Api<
  SecurityDataType extends unknown,
> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags City
     * @name CityGetCurrentCities
     * @request GET:/api/City/GetCurrentCities
     */
    cityGetCurrentCities: (params: RequestParams = {}) =>
      this.request<CityLocation[], any>({
        path: `/api/City/GetCurrentCities`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags City
     * @name CityGetFutureCities
     * @request GET:/api/City/GetFutureCities
     */
    cityGetFutureCities: (params: RequestParams = {}) =>
      this.request<CityLocation[], any>({
        path: `/api/City/GetFutureCities`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags City
     * @name CityCreateCityLocation
     * @request POST:/api/City/CreateCityLocation
     */
    cityCreateCityLocation: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/City/CreateCityLocation`,
        method: "POST",
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripGetAllTrips
     * @request GET:/api/SkiTrip/GetAllTrips
     */
    skiTripGetAllTrips: (params: RequestParams = {}) =>
      this.request<SkiTrip[], any>({
        path: `/api/SkiTrip/GetAllTrips`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripGetTripById
     * @request GET:/api/SkiTrip/GetTripById
     */
    skiTripGetTripById: (
      query?: {
        /** @format int32 */
        id?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<SkiTrip, any>({
        path: `/api/SkiTrip/GetTripById`,
        method: "GET",
        query: query,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripCreateTrip
     * @request POST:/api/SkiTrip/CreateTrip
     */
    skiTripCreateTrip: (data: SkiTrip, params: RequestParams = {}) =>
      this.request<SkiTrip, any>({
        path: `/api/SkiTrip/CreateTrip`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripUpdateTrip
     * @request PUT:/api/SkiTrip/UpdateTrip
     */
    skiTripUpdateTrip: (
      data: SkiTrip,
      query?: {
        /** @format int32 */
        id?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<SkiTrip, any>({
        path: `/api/SkiTrip/UpdateTrip`,
        method: "PUT",
        query: query,
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripDeleteTrip
     * @request DELETE:/api/SkiTrip/DeleteTrip
     */
    skiTripDeleteTrip: (
      query?: {
        /** @format int32 */
        id?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/SkiTrip/DeleteTrip`,
        method: "DELETE",
        query: query,
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripGetTripsByUserId
     * @request GET:/api/SkiTrip/GetTripsByUserId
     */
    skiTripGetTripsByUserId: (
      query?: {
        /** @format int32 */
        userId?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<SkiTrip[], any>({
        path: `/api/SkiTrip/GetTripsByUserId`,
        method: "GET",
        query: query,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags SkiTrip
     * @name SkiTripGetTripsByLocation
     * @request GET:/api/SkiTrip/GetTripsByLocation
     */
    skiTripGetTripsByLocation: (
      query?: {
        location?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<SkiTrip[], any>({
        path: `/api/SkiTrip/GetTripsByLocation`,
        method: "GET",
        query: query,
        format: "json",
        ...params,
      }),
  };
  getUsers = {
    /**
     * No description
     *
     * @tags User
     * @name UserGetUsers
     * @request GET:/GetUsers
     */
    userGetUsers: (
      query?: {
        /**
         * @format int32
         * @default 1
         */
        page?: number;
        /**
         * @format int32
         * @default 10
         */
        resultsPerPage?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<User[], any>({
        path: `/GetUsers`,
        method: "GET",
        query: query,
        format: "json",
        ...params,
      }),
  };
  getUserDetailsById = {
    /**
     * No description
     *
     * @tags User
     * @name UserGetUserDetailsById
     * @request GET:/GetUserDetailsById
     */
    userGetUserDetailsById: (
      query?: {
        /** @format int32 */
        userId?: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<UserGearDTO[], any>({
        path: `/GetUserDetailsById`,
        method: "GET",
        query: query,
        format: "json",
        ...params,
      }),
  };
  createUser = {
    /**
     * No description
     *
     * @tags User
     * @name UserCreateUser
     * @request POST:/CreateUser
     */
    userCreateUser: (
      query?: {
        FirstName?: string;
        LastName?: string;
        Email?: string;
        PhoneNumber?: string;
        HomeAddress?: string;
        AddressCity?: string;
        AddressState?: string;
        AddressZipCode?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<User, any>({
        path: `/CreateUser`,
        method: "POST",
        query: query,
        format: "json",
        ...params,
      }),
  };
}
