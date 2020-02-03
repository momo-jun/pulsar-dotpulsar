/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

﻿using System;
using System.Security.Cryptography.X509Certificates;

namespace DotPulsar.Abstractions
{
    /// <summary>
    /// A pulsar client building abstraction.
    /// </summary>
    public interface IPulsarClientBuilder
    {
        /// <summary>
        /// Authenticate using a client certificate. This is optional.
        /// </summary>
        IPulsarClientBuilder AuthenticateUsingClientCertificate(X509Certificate2 clientCertificate);

        /// <summary>
        /// Authenticate using a (JSON Web) token. This is optional.
        /// </summary>
        IPulsarClientBuilder AuthenticateUsingToken(string token);

        /// <summary>
        /// Set connection encryption policy. The default is 'EnforceUnencrypted' if the ServiceUrl scheme is 'pulsar' and 'EnforceEncrypted' if it's 'pulsar+ssl'.
        /// </summary>
        IPulsarClientBuilder ConnectionSecurity(EncryptionPolicy encryptionPolicy);

        /// <summary>
        /// The time to wait before retrying an operation or a reconnect. The default is 3 seconds.
        /// </summary>
        IPulsarClientBuilder RetryInterval(TimeSpan interval);

        /// <summary>
        /// The service URL for the Pulsar cluster. The default is "pulsar://localhost:6650".
        /// </summary>
        IPulsarClientBuilder ServiceUrl(Uri uri);

        /// <summary>
        /// Add a trusted certificate authority. This is optional.
        /// </summary>
        IPulsarClientBuilder TrustedCertificateAuthority(X509Certificate2 trustedCertificateAuthority);

        /// <summary>
        /// Verify the certificate authority. The default is 'true'.
        /// </summary>
        IPulsarClientBuilder VerifyCertificateAuthority(bool verifyCertificateAuthority);

        /// <summary>
        /// Verify the certificate name with the hostname. The default is 'false'.
        /// </summary>
        IPulsarClientBuilder VerifyCertificateName(bool verifyCertificateName);

        /// <summary>
        /// Create the client.
        /// </summary>
        IPulsarClient Build();
    }
}
