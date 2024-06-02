// Types
export interface RequestSuccessResponse {
    [key: string]: any;
}

export interface RequestErrorResponse {
    [key: string]: any;
}

type RequestOptions = Record<string, any>;
type RequestMethod = 'GET' | 'PATCH' | 'POST' | 'PUT' | 'DELETE';

class ApiRequest {
    baseUrl: string;

    constructor() {
        this.baseUrl = 'http://localhost:5248';
    }

    private async $baseMethod<T>(
        url: string,
        method: RequestMethod,
        options?: RequestOptions,
        body?: any,
    ): Promise<RequestSuccessResponse | RequestErrorResponse> {
        try {
            const res: T = await $fetch(this.baseUrl + url, {
                ...options,
                credentials: 'include',
                method,
                body,
            });

            return {
                data: res,
            };
        } catch (error: RequestErrorResponse | any) {
            return {
                error: error?.response,
            };
        }
    }

    async $get<T>(url: string, options?: RequestOptions): Promise<RequestSuccessResponse | RequestErrorResponse> {
        return await this.$baseMethod<T>(url, 'GET', options);
    }

    async $post<T>(url: string, body?: any, options?: RequestOptions): Promise<RequestSuccessResponse | RequestErrorResponse> {
        return await this.$baseMethod<T>(url, 'POST', options, body);
    }

    async $patch<T>(url: string, body?: any, options?: RequestOptions): Promise<RequestSuccessResponse | RequestErrorResponse> {
        return await this.$baseMethod<T>(url, 'PATCH', options, body);
    }

    async $delete<T>(url: string, options?: RequestOptions): Promise<RequestSuccessResponse | RequestErrorResponse> {
        return await this.$baseMethod<T>(url, 'DELETE', options);
    }
}

export default defineNuxtPlugin(() => {
    const request = new ApiRequest();

    return {
        provide: {
            request,
        },
    };
});
